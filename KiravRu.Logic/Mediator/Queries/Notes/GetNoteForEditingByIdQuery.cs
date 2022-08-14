using MediatR;

namespace KiravRu.Logic.Mediator.Queries.Notes
{
    public class GetNoteForEditingByIdQuery : IRequest<GetNoteForEditingByIdQueryResult>
    {
        public int NoteId { get; set; }
    }
}
