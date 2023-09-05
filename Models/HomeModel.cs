using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GGJWeb.Models
{
    public class HomeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? JamStart { get; set; }

        public IEnumerable<Post>? posts { get; set; }
    }
}
