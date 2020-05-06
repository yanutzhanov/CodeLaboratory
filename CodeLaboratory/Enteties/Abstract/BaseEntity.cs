using System.ComponentModel.DataAnnotations;

namespace CodeLaboratory.Enteties.Abstract
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
