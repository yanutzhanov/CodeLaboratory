using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CodeLaboratory.Enteties.Abstract;

namespace CodeLaboratory.Enteties
{
    [Table("projects")]
    public class ProjectEntity : BaseEntity
    {
        public ProjectEntity()
        {
            UserProjects = new List<UserProjectEntity>();
        }
        public string Name { get; set; }
        public int MaxPeople { get; set; }
        public string Description { get; set; }
        public string GitHub { get; set; }
        public string Discord { get; set; }
        public string OwnerId { get; set; }
        public UserEntity Owner { get; set; }
        public bool Finished { get; set; } = false;
        public string Language { get; set; }
        public IEnumerable<UserProjectEntity> UserProjects { get; set; }
    }
}
