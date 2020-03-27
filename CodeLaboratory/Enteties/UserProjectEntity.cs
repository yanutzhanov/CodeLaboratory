using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CodeLaboratory.Enteties
{
    [Table("user_projects")]
    public class UserProjectEntity
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }

        [JsonIgnore]
        public UserEntity User { get; set; }
        [JsonIgnore]
        public ProjectEntity Project { get; set; }
    }
}
