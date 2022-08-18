using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Interface.Categories;
using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Interface.Users;
using KiravRu.Logic.Mediator.Queries.Notes;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Notes
{
    public class GetNoteForEditingByIdQueryHandler : IRequestHandler<GetNoteForEditingByIdQuery, GetNoteForEditingByIdQueryResult>
    {
        private readonly INoteRepository _noteRepository;
        private readonly INoteAccessRepository _noteAccessRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICategoryRepository _categoryRepository;

        public GetNoteForEditingByIdQueryHandler(INoteRepository noteRepository, INoteAccessRepository noteAccessRepository,
            IRoleRepository roleRepository, ICategoryRepository categoryRepository)
        {
            _noteRepository = noteRepository;
            _noteAccessRepository = noteAccessRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<GetNoteForEditingByIdQueryResult> Handle(GetNoteForEditingByIdQuery request, CancellationToken cancellationToken)
        {
            var note = new Note();
            var noteRoles = await _noteAccessRepository.GetRolesAsync(request.NoteId, cancellationToken);
            var allRoles = await _roleRepository.GetRolesAsync(cancellationToken);

            var listRoles = new NoteRoles
            {
                UserRoles = noteRoles,
                AllRoles = allRoles
            };

            if (request.NoteId != 0)
            {
                note = await _noteRepository.GetNoteByIdAsync(request.NoteId, cancellationToken);
            }
            var listCategory = await _categoryRepository.OrderAllCategoryAsync(0, cancellationToken);

            return new GetNoteForEditingByIdQueryResult(note, listCategory, listRoles);
        }
    }
}