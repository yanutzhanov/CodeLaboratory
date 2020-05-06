using System.Collections.Generic;
using System.Linq;

namespace CodeLaboratory.Domain
{
    public class User
    {
        public User()
        {
            
            Projects = new List<Project>();
        }
        public string Login { get; set; }
        public string Email { get; set; }
        public string GitHub { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public int CountOfFinishedProjects
        {
            get
            {
                return Projects.Where(p => p.Finished == true).Count();
            }
        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Discord { get; set; }
        public string Avatar { get; set; }
    }
}
