using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindWebApp.Components.Pages
{
	public partial class ProductPage
	{
		[Inject]
		IJSRuntime JSRuntime { get; set; }

		[Inject]
		ProductServices ProductServices { get; set; }
		[Inject]
		CategoryServices CategoryServices { get; set; }
		[Inject]
		SupplierServices SupplierServices { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Parameter]
		public int ProductId { get; set; }

		private List<Category> Categories { get; set; } = new();
		private List<Supplier> Suppliers { get; set; } = new();

		private Product? Product { get; set; }

		private List<string> Errors { get; set; } = new();
		private string FeedbackMessage { get; set; }

		protected override Task OnInitializedAsync()
		{
			Product = new Product();

			return Task.Run(() =>
			{
				Categories = CategoryServices.GetAllCategories();
				Suppliers = SupplierServices.GetAllSuppliers();

				if (ProductId != 0)
				{
					// View/edit a product
					Product = ProductServices.GetProductById(ProductId);

					if (Product == null)
					{
						Errors.Add($"No product found for id {ProductId}");
						Product = new Product();
					}
				}
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private bool IsValidForm()
		{
			Errors.Clear();

			if (string.IsNullOrWhiteSpace(Product!.ProductName))
			{
				Errors.Add("Product name cannot be empty");
			}

			if (string.IsNullOrWhiteSpace(Product!.QuantityPerUnit))
			{
				Errors.Add("Quantity per unit cannot be empty");
			}

			if (Product.UnitPrice <= 0)
			{
				Errors.Add("Unit price must be positive");
			}

			if (Product.UnitsOnOrder < 0)
			{
				Errors.Add("Units on order cannot be negative");
			}

			if (Product.CategoryId == 0)
			{
				Errors.Add("Must select a category");
			}

			if (Product.SupplierId == 0)
			{
				Errors.Add("Must select a supplier");
			}

			return Errors.Count() == 0;
		}

		/// <summary>
		/// 
		/// </summary>
		//private void HandleDiscontinue()
		//{
		//	if (Product.ProductId != 0)
		//	{
		//		try
		//		{
		//			ProductServices.DiscontinueProduct(Product);
		//			FeedbackMessage = "Product successfully discontinued";
		//		}
		//		catch (Exception ex)
		//		{
		//			Errors.Add(ex.Message);
		//		}
		//	}
		//}

		private async Task HandleDiscontinue()
		{
			if (Product!.ProductId != 0)
			{
				// Make a JS call to confirm whether to discontinue or not
				object[] message = new[] { "Are you sure you want to discontinue?" };
				
				if (await JSRuntime.InvokeAsync<bool>("confirm", message))
				{
					try
					{
						ProductServices.DiscontinueProduct(Product);
						FeedbackMessage = "Product successfully discontinued";
					}
					catch (Exception ex)
					{
						Errors.Add(ex.Message);
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void HandleSaveProduct()
		{
			if (IsValidForm())
			{
				if (Product!.ProductId == 0)
				{
					try
					{
						ProductServices.AddProduct(Product);
						FeedbackMessage = "Product successfully added";
						NavigationManager.NavigateTo($"/product/{Product!.ProductId}");
					}
					catch (Exception ex)
					{
						Errors.Add(ex.Message);
					}
				}
				else
				{
					try
					{
						ProductServices.UpdateProduct(Product);
						FeedbackMessage = "Product successfully updated";
					}
					catch (Exception ex)
					{
						Errors.Add(ex.Message);
					}
				}
			}
		}
	}
}
