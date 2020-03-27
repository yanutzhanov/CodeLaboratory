using System.Collections.Generic;

namespace CodeLaboratory.Domain
{
    public class Project
    {
        public Project()
        {
            UserProjects = new List<UserProject>();
        }
        public string Name { get; set; }
        public int MaxPeople { get; set; }
        public string Description { get; set; }
        public string GitHub { get; set; }
        public string Discord { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public bool Finished { get; set; } = false;
        public string Language { get; set; }
        public IEnumerable<UserProject> UserProjects { get; set; }
    }
}
