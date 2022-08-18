using KiravRu.Logic.Interface.HistoryChanges;
using KiravRu.Logic.Mediator.Queries.Arts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Arts
{
    public class CheckRemoteIpAddressOfUserQueryHandler : IRequestHandler<CheckRemoteIpAddressOfUserQuery, string>
    {
        private readonly IHistoryChangeRepository _historyChangeRepository;

        public CheckRemoteIpAddressOfUserQueryHandler(IHistoryChangeRepository historyChangeRepository)
        {
            _historyChangeRepository = historyChangeRepository;
        }

        public async Task<string> Handle(CheckRemoteIpAddressOfUserQuery request, CancellationToken cancellationToken)
        {
            DateTime lastDate = await _historyChangeRepository.GetLastDateTimeByIpAddressAsync(request.RemoteIpAddress, cancellationToken);
            if (DateTime.Now < lastDate.AddMinutes(15))
            {
                var span = lastDate.AddMinutes(15) - DateTime.Now;
                return string.Format("{0}", span.Minutes);
            }
            return "Ok";
        }
    }
}