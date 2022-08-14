using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Interface.Users;
using KiravRu.Logic.Mediator.Queries.Notes;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Notes
{
    public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, GetNotesQueryResult>
    {
        private readonly INoteRepository _noteRepository;
        private readonly INoteAccessRepository _noteAccessRepository;
        private readonly IRoleRepository _roleRepository;

        public GetNotesQueryHandler(INoteRepository noteRepository, INoteAccessRepository noteAccessRepository,
            IRoleRepository roleRepository)
        {
            _noteRepository = noteRepository;
            _noteAccessRepository = noteAccessRepository;
            _roleRepository = roleRepository;
        }

        public async Task<GetNotesQueryResult> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // get Ids of roles of current user
                var roleIds = await _roleRepository.GetRoleIdsByRoleNamesAsync(request.UserRoles, cancellationToken);
                // get a list of Id article
                var listOfNotes = await _noteAccessRepository.GetListOfNotesByRoleIds(roleIds, cancellationToken);
                // get a list of article
                var notes = _noteRepository.GetNotesWithFilter(request.NoteFilter, listOfNotes);

                var result = new GetNotesQueryResult();
                result.TotalCount = notes.TotalRecordCount;
                result.AddItems(notes.List);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("There are problems in GetNotesQueryHandler", ex);
            }
        }
    }
}