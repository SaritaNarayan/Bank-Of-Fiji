using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
namespace BoF.Web.Models
{
    public class LoginModel
    {
        [Key]
        public int UserId { get; set; }

        public string Role { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class LoginDBContext : DbContext
    {
        //LoginModel Refers to name of Model Class
        public DbSet<LoginModel> Logins { get; set; }
    }
}