using Microsoft.Extensions.DependencyInjection;
using WestWindSystem.BLL;
using WestWindSystem.Entities;
using WestWindSystem;
using Microsoft.EntityFrameworkCore;

// Use a service provider
IServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.WWBackendDependencies(options => 
	options.UseSqlServer("Data Source=.;Initial Catalog=WestWind;Integrated Security=True;TrustServerCertificate=true"));
ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

// Using a services class
CustomerServices customerServices = serviceProvider.GetService<CustomerServices>();// must come from a ServiceProvider 

foreach (var customer in customerServices!.GetAllCustomers())
{
	Console.WriteLine(customer.ContactName);
}

Customer? maria = customerServices.GetCustomerByContactName("maria");

if (maria != null)
{
	Console.WriteLine($"Maria works for: {maria.CompanyName}");
}
else
{
	Console.WriteLine("Maria was not found.");
}

// Using RAW context
/*
foreach (var customer in context.Customers)
{
	Console.WriteLine(customer.ContactName);
}

// Run LINQ queries

// Search by partial contact name
Customer firstCustomer = context.Customers.Where(c => c.ContactName.Contains("m")).FirstOrDefault();


Console.WriteLine($"The first customer is: {firstCustomer.ContactName}");
*/