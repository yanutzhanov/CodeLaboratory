using System.Text.Json.Serialization;

namespace CodeLaboratory.Domain
{
    public class UserProject
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
    }
}
