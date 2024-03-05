using WestWindSystem.DAL;
using WestWindSystem.Entities;

WestWindContext context = new WestWindContext();

foreach (var customer in context.Customers)
{
	Console.WriteLine(customer.ContactName);
}

// Run LINQ queries

Customer firstCustomer = context.Customers.First();

Console.WriteLine($"The first customer is: {firstCustomer.ContactName}");