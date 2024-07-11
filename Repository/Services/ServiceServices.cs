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
        DbSet<User> dbSet;

        public ServiceServices()
        {
            context = new HotelManagementContext();
            dbSet = context.Set<User>();
        }

        public List<Service> SearchServices(string searchTerm)
        {
            return context.Services
                .Where(s => s.ServiceName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

    }
}
