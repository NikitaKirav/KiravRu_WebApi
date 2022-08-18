using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Mediator.Commands.Notes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Notes
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
    {
        private readonly INoteRepository _noteRepository;

        public DeleteNoteCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetNoteByIdAsync(request.NoteId, cancellationToken);
            if (note == null)
            {
                throw new KeyNotFoundException("Note dosn't exist with Id = " + request.NoteId);
            }
            _noteRepository.Delete(note);
            await _noteRepository.SaveChanges(cancellationToken);

            return true;
        }
    }
}