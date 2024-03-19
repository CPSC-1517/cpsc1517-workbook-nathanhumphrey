
using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;

namespace WestWindWebApp.Components.Pages
{
	public partial class Customer
	{
		[Parameter]
		public string? CustomerId { get; set; }

		public WestWindSystem.Entities.Customer? CurrentCustomer { get; set; }

		[Inject]
		CustomerServices? CustomerServices { get; set; }

		protected override void OnInitialized()
		{
			CurrentCustomer = CustomerServices.GetCustomerById(CustomerId);
			base.OnInitialized();
		}
	}
}
