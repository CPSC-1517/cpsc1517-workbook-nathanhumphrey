using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
	public class SupplierServices
	{
		private readonly WestWindContext _context;
		internal SupplierServices(WestWindContext context)
		{
			_context = context;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<Supplier> GetAllSuppliers()
		{
			return _context.Suppliers.OrderBy(s => s.CompanyName).ToList<Supplier>();
		}
	}
}
