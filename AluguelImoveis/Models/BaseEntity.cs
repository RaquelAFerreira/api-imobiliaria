using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelImoveis.Models
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("NEWID()")]
        public Guid Id { get; set; }
    }
}