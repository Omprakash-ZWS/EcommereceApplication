using EcommerceApplication.Context;
using EcommerceApplication.Models;
using EcommerceApplication.Service.Interface;

namespace EcommerceApplication.Service.Infrastructure
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(ECommDbContext context) : base(context)
		{
		}
	}
}
