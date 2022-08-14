using KiravRu.Logic.Domain.HistoryChanges;
using MediatR;

namespace KiravRu.Logic.Mediator.Commands.Arts
{
    public class AddNewHistoryNoteCommand : IRequest<bool>
    {
        public HistoryChange HistoryChange { get; set; }
    }
}
