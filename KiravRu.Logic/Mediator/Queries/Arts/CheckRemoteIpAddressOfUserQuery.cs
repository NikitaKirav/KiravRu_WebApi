using MediatR;

namespace KiravRu.Logic.Mediator.Queries.Arts
{
    public class CheckRemoteIpAddressOfUserQuery : IRequest<string>
    {
        public string RemoteIpAddress { get; set; }

        public CheckRemoteIpAddressOfUserQuery(string remoteIpAddress)
        {
            RemoteIpAddress = remoteIpAddress;
        }
    }
}
