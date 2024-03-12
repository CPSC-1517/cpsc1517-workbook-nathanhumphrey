using WestWindSystem.BLL;
using WestWindSystem.DAL;
using WestWindSystem.Entities;

WestWindContext context = new WestWindContext();

// Using a services class
CustomerServices customerServices = new CustomerServices(context);

foreach (var customer in customerServices.GetAllCustomers())
{
	Console.WriteLine(customer.ContactName);
}

Customer? maria = customerServices.GetCustomerByContactName("maria");

Console.WriteLine($"Maria works for: {maria.CompanyName}");

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