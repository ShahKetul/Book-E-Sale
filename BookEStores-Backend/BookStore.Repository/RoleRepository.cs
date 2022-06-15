using BookStore.Models.Model;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class RoleRepository: BaseRepository
    {
        public ListResponse<Role> GetRoles(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Roles.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Role> roles = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Role>()
            {
                records = roles,
                totalRecords = totalRecords
            };

        }

        public Role GetRole(int id)
        {
            return _context.Roles.FirstOrDefault(c => c.Id == id);
        }

        public Role AddRole(Role category)
        {
            var entry = _context.Roles.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Role UpdateRole(Role category)
        {
            var entry = _context.Roles.Update(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteRole(int id)
        {
            var category = _context.Roles.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return false;
            _context.Roles.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
