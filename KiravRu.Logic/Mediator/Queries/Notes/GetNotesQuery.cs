using KiravRu.Logic.Domain.Notes;
using MediatR;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Queries.Notes
{
    public class GetNotesQuery : IRequest<GetNotesQueryResult>
    {
        public NoteFilter NoteFilter { get; set; }
        public IList<string> UserRoles { get; set; }
        public string DefaultSort { get; set; }
    }
}
