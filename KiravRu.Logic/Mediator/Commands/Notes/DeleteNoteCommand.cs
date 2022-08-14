using MediatR;

namespace KiravRu.Logic.Mediator.Commands.Notes
{
    public class DeleteNoteCommand : IRequest<bool>
    {
        public int NoteId { get; set; }
    }
}
