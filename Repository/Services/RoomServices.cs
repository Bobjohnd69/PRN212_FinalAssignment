using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class RoomServices:GenericRepository<Room>
    {
        HotelManagementContext context;
        DbSet<Room> dbSet;

        public RoomServices()
        {
            context = new HotelManagementContext();
            dbSet = context.Set<Room>();
        }

        public List<Room> GetRooms()
        {
            return dbSet.ToList();
        }

        public List<Room> GetByStatus(int status)
        {
            return dbSet.Where(x => x.RoomStatus == status).ToList();
        }

        public Room GetByRoomId(int roomId)
        {
            return dbSet.FirstOrDefault(x => x.RoomId == roomId);
        }
    }
}
