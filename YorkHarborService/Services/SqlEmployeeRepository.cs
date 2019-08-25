using YorkHarborService.Data;
using YorkHarborService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YorkHarborService.Services
{
    public class SqlEmployeeRepository : IRepository<Employee>
	{
		private YorkHarborServiceDbContext _context;
        private readonly ILogger _logger;

        public SqlEmployeeRepository(YorkHarborServiceDbContext context, ILogger<SqlEmployeeRepository> logger)
		{
			_context = context;
			_logger = logger;
		}

		public bool Add(Employee item)
		{
			try
			{
				_context.Employees.Add(item);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed to add Employee item to the database: " + ex.Message);
				return false;
			}
		}

		public bool Delete(Employee Item)
		{
			try
			{
				Employee employee = Get(Item.Id);
				if (employee != null)
				{
					_context.Employees.Remove(Item);
					_context.SaveChanges();
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to delete employee: " + ex.Message);
				return false;
			}
		}

		public bool Edit(Employee item)
		{
			try
			{
				_context.Employees.Update(item);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to save employee: " + ex.Message);
			}
			return false;
		}

		public Employee Get(int id)
		{
			if (_context.Employees.Count(x => x.Id == id) > 0)
			{
				return _context.Employees.First(x => x.Id == id);
			}
			return null;
		}

        public Employee Get(int? id)
        {
            if(id==null)
            {
                throw new ArgumentNullException();
            }
            if (_context.Employees.Count(x => x.Id == id) > 0)
            {
                return _context.Employees.FirstOrDefault(x => x.Id == id);
            }
            return null;
        }

        public IEnumerable<Employee> GetAll()
		{
			return _context.Employees;
		}
	}
}
