using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class RoomServiceServices:GenericRepository<RoomService>
    {
        HotelManagementContext context;
        DbSet<RoomService> dbSet;

        public RoomServiceServices()
        {
            context = new HotelManagementContext();
            dbSet = context.Set<RoomService>();
        }

        public List<RoomService> GetByStatus(int status)
        {
            return dbSet.Where(x => x.Status == status).ToList();
        }
    }
}
