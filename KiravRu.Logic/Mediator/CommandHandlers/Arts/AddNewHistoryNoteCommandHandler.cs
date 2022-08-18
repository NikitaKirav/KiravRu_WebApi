using KiravRu.Logic.Interface.HistoryChanges;
using KiravRu.Logic.Mediator.Commands.Arts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Arts
{
    public class AddNewHistoryNoteCommandHandler : IRequestHandler<AddNewHistoryNoteCommand, bool>
    {
        private readonly IHistoryChangeRepository _historyChangeRepository;

        public AddNewHistoryNoteCommandHandler(IHistoryChangeRepository historyChangeRepository)
        {
            _historyChangeRepository = historyChangeRepository;
        }

        public async Task<bool> Handle(AddNewHistoryNoteCommand request, CancellationToken cancellationToken)
        {
            _historyChangeRepository.AddToSet(request.HistoryChange);
            await _historyChangeRepository.SaveChanges(cancellationToken);

            return true;
        }
    }
}
