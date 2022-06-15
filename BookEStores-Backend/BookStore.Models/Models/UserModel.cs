using BookStore.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(User user)
        {
            id = user.Id;
            firstName = user.Firstname;
            lastName = user.Lastname;
            email = user.Email;
            password = user.Password;
            roleId = user.Roleid;
        }
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }

    }
}
