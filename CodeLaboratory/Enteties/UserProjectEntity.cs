using CodeLaboratory.Enteties.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeLaboratory.Enteties
{
    [Table("user_projects")]
    public class UserProjectEntity : BaseEntity
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }

        [JsonIgnore]
        public UserEntity User { get; set; }
        [JsonIgnore]
        public ProjectEntity Project { get; set; }
    }
}
