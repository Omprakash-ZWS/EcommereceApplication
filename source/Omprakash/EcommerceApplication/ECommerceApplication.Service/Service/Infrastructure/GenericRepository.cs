
using EcommerceApplication.Service.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Service.Infrastructure
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ECommDbContext _context;
		//private readonly ECommDbContext _context;
		private readonly DbSet<T> _dbSet;


		public GenericRepository(ECommDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}
		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			this._dbSet.Remove(entity);
		}

		

		public IEnumerable<T> GetAll()
		{
			return this._dbSet.ToList();
		}

		public T GetById(int id)
		{
			return this._dbSet.Find(id);
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}

	}
}
