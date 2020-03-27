using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CodeLaboratory.Enteties.Abstract;

namespace CodeLaboratory.Enteties
{
    [Table("users")]
    public class UserEntity : BaseEntity
    {
        public UserEntity()
        {
            UserProjects = new List<UserProjectEntity>();
        }
        public string Login { get; set; }
        public string Email { get; set; }
        public string GitHub { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public IEnumerable<UserProjectEntity> UserProjects { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Discord { get; set; }
        public string Avatar { get; set; }
    }
}
