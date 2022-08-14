
namespace KiravRu.Controllers.v1.Model
{
    public class LoginRequestModel
    {
        //[Required]
        //[Display(Name = "UserName")]
        public string UserName { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
