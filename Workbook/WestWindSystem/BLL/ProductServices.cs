using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
	public class ProductServices
	{
		private readonly WestWindContext _context;
		internal ProductServices(WestWindContext context)
		{
			_context = context;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<Product> GetProductsByCategoryId(int id)
		{
			return _context.Products
				.Include(p => p.Supplier)
				.Where(p => p.CategoryId == id)
				.OrderBy(p => p.ProductName)
				.ToList<Product>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partial"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public List<Product> GetProductsByNameOrSupplierName(string partial)
		{
			if (string.IsNullOrWhiteSpace(partial))
			{
				throw new ArgumentNullException("Partial argument cannot be empty", new ArgumentException());
			}

			partial = partial.ToLower();

			return _context.Products
				.Include(p => p.Supplier)
				.Where(p => p.ProductName.ToLower().Contains(partial) ||
					p.Supplier.CompanyName.ToLower().Contains(partial))
				.OrderBy(p => p.ProductName)
				.ToList<Product>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Product? GetProductById(int id)
		{
			return _context.Products.Where(p => p.ProductId == id).FirstOrDefault();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="product"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void AddProduct(Product product)
		{
			if (product == null)
			{
				throw new ArgumentNullException("Product cannot be null", new ArgumentNullException());
			}
			_context.Products.Add(product);
			_context.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="product"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void UpdateProduct(Product product)
		{
			if (product == null)
			{
				throw new ArgumentNullException("Product cannot be null", new ArgumentNullException());
			}
			_context.Products.Update(product);
			_context.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="product"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void DiscontinueProduct(Product product)
		{
			if (product == null)
			{
				throw new ArgumentNullException("Product cannot be null", new ArgumentNullException());
			}

			product.Discontinued = true;
			UpdateProduct(product);

		}
	}
}
