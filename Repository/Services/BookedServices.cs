using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class BookedServices : GenericRepository<Booked>
    {
        HotelManagementContext context;
        DbSet<Booked> dbSet;

        public BookedServices() {
            context = new HotelManagementContext();
            dbSet = context.Set<Booked>();
        }

        public List<Booked> GetByStatus(int status)
        {
           return dbSet.Where(x => x.Status == status).ToList();
        }

        public Booked GetByBookId(Guid bookedId) {

            return dbSet.FirstOrDefault(x => x.BookedId == bookedId);
        }
    }
}
