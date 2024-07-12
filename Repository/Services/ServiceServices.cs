using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class ServiceServices:GenericRepository<Service>
    {
        HotelManagementContext context;
        DbSet<Service> dbSet;

        public ServiceServices()
        {
            context = new HotelManagementContext();
            dbSet = context.Set<Service>();
        }

        public List<Service> SearchServices(string searchTerm)
        public List<Service> SearchServices(string searchTerm)
        {
            return dbSet.Where(s => s.ServiceName.Contains(searchTerm)).ToList();
        }

        HotelManagementContext context;
        DbSet<Service> dbSet;

        public ServiceServices()
        {
            context = new HotelManagementContext();
            dbSet = context.Set<Service>();
        }

        public List<Service> GetAllServices()
        {
            return dbSet.ToList();
        }
    }
}
