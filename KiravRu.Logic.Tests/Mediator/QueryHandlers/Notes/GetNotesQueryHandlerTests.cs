using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Interface.Users;
using KiravRu.Logic.Mediator.Queries.Notes;
using KiravRu.Logic.Mediator.QueryHandlers.Notes;
using KiravRu.Logic.Tests.Mocks;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace KiravRu.Logic.Tests.Mediator.QueryHandlers.Notes
{
    public class GetNotesQueryHandlerTests
    {
        private readonly Mock<INoteRepository> _noteRepository;
        private readonly Mock<INoteAccessRepository> _noteAccessRepository;
        private readonly Mock<IRoleRepository> _roleRepository;

        public GetNotesQueryHandlerTests()
        {
            _noteRepository = MockNoteRepository.GetMockNoteRepository();
            _noteAccessRepository = MockNoteAccessRepository.GetMockNoteAccessRepository();
            _roleRepository = MockRoleRepository.GetMockRoleRepository();
        }

        [Fact]
        public async Task testGetNotes()
        {
            var handler = new GetNotesQueryHandler(_noteRepository.Object, _noteAccessRepository.Object, _roleRepository.Object);

            var request = new GetNotesQuery() {
                NoteFilter = new NoteFilter(), 
                UserRoles = new List<string> { "admin" },
                DefaultSort = "Name"
            };
            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<GetNotesQueryResult>();
            result.TotalCount.ShouldBe(3);
        }
    }
}
