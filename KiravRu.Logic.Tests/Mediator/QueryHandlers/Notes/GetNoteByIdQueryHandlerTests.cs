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
    public class GetNoteByIdQueryHandlerTests
    {
        private readonly Mock<INoteRepository> _noteRepository;
        private readonly Mock<INoteAccessRepository> _noteAccessRepository;
        private readonly Mock<IRoleRepository> _roleRepository;

        public GetNoteByIdQueryHandlerTests()
        {
            _noteRepository = MockNoteRepository.GetMockNoteRepository();
            _noteAccessRepository = MockNoteAccessRepository.GetMockNoteAccessRepository();
            _roleRepository = MockRoleRepository.GetMockRoleRepository();
        }

        [Fact]
        public async Task testGetNoteById()
        {
            var handler = new GetNoteByIdQueryHandler(_noteRepository.Object, _noteAccessRepository.Object, _roleRepository.Object);

            var request = new GetNoteByIdQuery() { NoteId = 1, UserRoles = new List<string> { "admin" } };
            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<GetNoteByIdQueryResult>();
            result.Name.ShouldBe("React - How use hooks");
        }
    }
}
