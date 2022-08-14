using MediatR;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Commands.Roles
{
    public class UpdateAccessCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
