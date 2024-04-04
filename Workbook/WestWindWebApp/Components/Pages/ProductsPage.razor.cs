using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindWebApp.Components.Pages
{
	public partial class ProductsPage
	{
		[Inject]
		CategoryServices CategoryServices { get; set; }

		[Inject]
		ProductServices ProductServices { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		// Required properties
		public List<Category> Categories { get; set; }
		public List<Product> Products { get; set; }

		[Parameter]
		public int CategoryId { get; set; }

		// Used for the partial search form
		public string PartialSearch { get; set; }

		protected override Task OnInitializedAsync()
		{
			return Task.Run(() =>
			{
				Categories = CategoryServices.GetAllCategories();

				if (CategoryId != 0)
				{
					Products = ProductServices.GetProductsByCategoryId(CategoryId);
				}
			});
		}

		/// <summary>
		/// 
		/// </summary>
		private void HandleCategorySelected()
		{
			if (CategoryId > 0)
			{
				Products = ProductServices.GetProductsByCategoryId(CategoryId);
				NavigationManager.NavigateTo($"/products/{CategoryId}");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void HandlePartialSearch()
		{
			if (!string.IsNullOrWhiteSpace(PartialSearch)) 
			{
				Products = ProductServices.GetProductsByNameOrSupplierName(PartialSearch);
				CategoryId = 0;
				NavigationManager.NavigateTo($"/products");
			}
			else
			{
				Products.Clear();
			}
		}
	}
}
