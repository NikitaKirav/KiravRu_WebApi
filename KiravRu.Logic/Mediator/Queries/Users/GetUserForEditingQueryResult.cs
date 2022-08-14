
namespace KiravRu.Logic.Mediator.Queries.Users
{
    public class GetUserForEditingQueryResult
    {
        public UserInfo User { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
