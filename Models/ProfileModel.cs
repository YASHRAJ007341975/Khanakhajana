using System.ComponentModel.DataAnnotations;

namespace Khanakhajana.Models
{
    public class ProfileModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }




    }
}
