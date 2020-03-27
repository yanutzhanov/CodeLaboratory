using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLaboratory.Domain
{
    public class User
    {
        public User()
        {
            
            UserProjects = new List<UserProject>();
        }
        public string Login { get; set; }
        public string Email { get; set; }
        public string GitHub { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public IEnumerable<UserProject> UserProjects { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Discord { get; set; }
        public string Avatar { get; set; }
    }
}
