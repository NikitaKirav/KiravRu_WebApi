using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Mediator.Commands.Notes;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Notes
{
    public class SaveNoteCommandHandler : IRequestHandler<SaveNoteCommand, int>
    {
        private readonly INoteRepository _noteRepository;
        private readonly INoteAccessRepository _noteAccessRepository;

        public SaveNoteCommandHandler(INoteRepository noteRepository, INoteAccessRepository noteAccessRepository)
        {
            _noteRepository = noteRepository;
            _noteAccessRepository = noteAccessRepository;
        }

        public async Task<int> Handle(SaveNoteCommand request, CancellationToken cancellationToken)
        {
            if (request.Note.Id == 0)
            {
                _noteRepository.AddToSet(request.Note);
                await _noteRepository.SaveChanges(cancellationToken);
            }
            else
            {
                _noteRepository.Update(request.Note);
                await _noteRepository.SaveChanges(cancellationToken);
            }
            _noteAccessRepository.ChangeRolesOfNote(request.Note.Id, request.Roles);
            await _noteAccessRepository.SaveChanges(cancellationToken);

            return request.Note.Id;
        }
    }
}