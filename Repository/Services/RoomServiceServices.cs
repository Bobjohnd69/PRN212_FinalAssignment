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

        public void Delete(Guid roomId, Guid serviceId)
        {
            var service = context.RoomServices.FirstOrDefault(rs => rs.RoomId == roomId && rs.ServiceId == serviceId);
            if (service != null)
            {
                context.RoomServices.Remove(service);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Service not found");
            }
        }

        public List<RoomService> GetByStatus(int status)
        {
            return dbSet.Where(x => x.Status == status).ToList();
        }
    }
}
