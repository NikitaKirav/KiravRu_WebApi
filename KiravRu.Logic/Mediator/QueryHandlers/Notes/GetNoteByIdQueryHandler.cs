using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Interface.Users;
using KiravRu.Logic.Mediator.Queries.Notes;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Notes
{
    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, GetNoteByIdQueryResult>
    {
        private readonly INoteRepository _noteRepository;
        private readonly INoteAccessRepository _noteAccessRepository;
        private readonly IRoleRepository _roleRepository;

        public GetNoteByIdQueryHandler(INoteRepository noteRepository, INoteAccessRepository noteAccessRepository,
            IRoleRepository roleRepository)
        {
            _noteRepository = noteRepository;
            _noteAccessRepository = noteAccessRepository;
            _roleRepository = roleRepository;
        }

        public async Task<GetNoteByIdQueryResult> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // get Ids of roles of current user
                var roleIds = await _roleRepository.GetRoleIdsByRoleNamesAsync(request.UserRoles, cancellationToken);
                // get a list of Id article
                var listOfNotes = await _noteAccessRepository.GetListOfNotesByRoleIds(roleIds, cancellationToken);
                // get an note
                var note = _noteRepository.GetNoteByIdWithAccess(request.NoteId, listOfNotes);

                if (note == null)
                {
                    throw new Exception(@"Note with Id=" + request.NoteId + " not found.");
                }

                return new GetNoteByIdQueryResult(note);
            }
            catch(Exception ex)
            {
                throw new Exception("There is a problem in GetNoteByIdQueryHandler", ex);
            }
        }
    }
}