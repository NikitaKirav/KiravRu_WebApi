using KiravRu.Logic.Mediator.Dto;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Queries.Users
{
    public class GetUserQueryResult
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<IdentityModel<string>> Roles { get; set; }
    }
}
