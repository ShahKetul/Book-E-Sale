using BookStore.Models.Model;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class UserRepository : BaseRepository
    {

        public ListResponse<User> GetUsers(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            if (pageIndex > 0)
            {
                var query = _context.Users.Where(c => keyword == null || c.Firstname.ToLower().Contains(keyword) || c.Lastname.ToLower().Contains(keyword)).AsQueryable();
                int totalRecords = query.Count();
                List<User> users = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new ListResponse<User>()
                {
                    records = users,
                    totalRecords = totalRecords,
                };
            }
            return null;
            

        }

        public User Login(User user)
        {
            return _context.Users.FirstOrDefault(c => c.Email.Equals(user.Email.ToLower()) && c.Password.Equals(user.Password));

        }

        public User Register(User model)
        {

            var entry = _context.Users.Add(model);
            _context.SaveChanges();
            return entry.Entity;
        }
        public User getUser(int id)
        {
            if (id > 0)
            {
                return _context.Users.Where(w => w.Id == id).FirstOrDefault();
            }
            return null;
        }

        public bool updateUser(User model)
        {
            if (model.Id > 0)
            {
                UserRepository _repository = new UserRepository();
                var user = _repository.getUser(model.Id);
                if (user == null)
                {
                    return false;
                }

                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        public bool deleteUser(User model)
        {
            if (model.Id > 0)
            {
                _context.Remove(model);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
