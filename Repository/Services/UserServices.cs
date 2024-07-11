using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class UserServices:GenericRepository<User>
    {

        HotelManagementContext context;
        DbSet<User> dbSet;

        public UserServices()
        {
            context = new HotelManagementContext();
            dbSet = context.Set<User>();
        }

        public List<User> GetUsers()
        {
            return dbSet.ToList();
        }

        public List<User> GetByStatus(int status)
        {
            return dbSet.Where(x => x.Status == status).ToList();
        }

        public User GetByEmailAndPassword(String email, String password)
        {
            User user = dbSet.FirstOrDefault(x => x.Email == email && x.Password == password);
            return user;
        }

        public User GetByUserID(Guid userID)
        {
            return dbSet.FirstOrDefault(x => x.UserId == userID);
        }
    }
}
