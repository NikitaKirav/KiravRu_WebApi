
namespace KiravRu.Controllers.v1.Model
{
    public class ChangePasswordRequestModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
