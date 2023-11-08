using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GGJWeb.Models
{
    public class HomeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? SignupLink { get; set; }

        public DateTime JamStart { get; set; }

        public bool HasNextPage { get; set; }

        public IEnumerable<Post>? posts { get; set; }
    }
}
