
namespace KiravRu.Controllers.v1.Model
{
    public class RegisterRequestModel
    {
        //[Required]
       //[Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[Display(Name = "Name")]
        public string UserName { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        //[Required]
        //[Compare("Password", ErrorMessage = "Passwords do not match")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Repeat password")]
        public string PasswordConfirm { get; set; }
    }
}
