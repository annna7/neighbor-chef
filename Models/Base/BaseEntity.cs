using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using neighbor_chef.Models.Base;

namespace neighbor_chef.Models.Base
{
    public class BaseEntity: IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}