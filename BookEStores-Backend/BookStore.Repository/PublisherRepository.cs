using BookStore.Models.Model;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    
    public class PublisherRepository:BaseRepository
    {

        public ListResponse<Publisher> GetPublishers(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Publishers.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            List<Publisher> result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            int totalrecords = query.Count();

            return new ListResponse<Publisher>()
            {
                records = result,
                totalRecords = totalrecords
            };
 
        }

        public Publisher GetPublisher(int id)
        {
            if (id > 0)
            {
                return _context.Publishers.FirstOrDefault(c => c.Id == id);
            }
            return null;
                
        }

        public Publisher AddPublisher(Publisher publisher)
        {
            var entry = _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return entry.Entity;
        }
        public bool updateUser(PublisherModel model)
        {
            if (model.Id > 0)
            {
                PublisherRepository _repository = new PublisherRepository();
                var user = _repository.GetPublisher(model.Id);
                if (user == null)
                {
                    return false;
                }

                user.Name = model.Name;
                user.Address = model.Address;
                user.Contact = model.Contact;

                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool deletPublisher(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(c => c.Id == id);
            if (publisher == null)
                return false;
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
            return true;
        }
    }
}
