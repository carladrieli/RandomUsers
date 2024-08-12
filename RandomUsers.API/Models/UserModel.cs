using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomUsers.API.Models
{
    public class UserModel
    {
        [Key]
        [Column("uuid")]
        public string Uuid { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("name_first")]
        public string FirstName { get; set; }

        [Column("name_last")]
        public string LastName { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("cell")]
        public string Cell { get; set; }
    }
}
