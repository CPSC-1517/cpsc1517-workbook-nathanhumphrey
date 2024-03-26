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
	}
}
