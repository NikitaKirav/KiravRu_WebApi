using MediatR;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Queries.Notes
{
    public class GetNoteByIdQuery : IRequest<GetNoteByIdQueryResult>
    {
        public int NoteId { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
