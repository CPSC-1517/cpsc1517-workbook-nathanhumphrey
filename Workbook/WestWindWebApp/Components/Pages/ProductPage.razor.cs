using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindWebApp.Components.Pages
{
	public partial class ProductPage
	{
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

		// TODO: add a discontinue button handler

		/// <summary>
		/// 
		/// </summary>
		private void HandleSaveProduct()
		{
			// TODO: create a validate method
			// IF VALID - is it new or is it edit?


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
