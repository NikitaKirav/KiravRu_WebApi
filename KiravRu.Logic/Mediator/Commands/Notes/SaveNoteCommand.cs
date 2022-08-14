using KiravRu.Logic.Domain.Notes;
using MediatR;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Commands.Notes
{
    public class SaveNoteCommand : IRequest<int>
    {
        public Note Note { get; set; }
        public List<string> Roles { get; set; }
    }
}
