using System.Collections.Generic;

namespace KiravRu.Controllers.v1.Model
{
    public class UpdateAccessRequestModel
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
