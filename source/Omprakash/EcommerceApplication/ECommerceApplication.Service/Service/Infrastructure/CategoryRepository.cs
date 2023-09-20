
using EcommerceApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Service.Infrastructure
{
	public interface CategoryRepository : IGenericRepository<ECommerceApplication.Domain.Entities.Category>, ICategoryRepository
	{
	}
}
