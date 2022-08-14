using KiravRu.Logic.Interface.Categories;
using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Interface.Users;
using KiravRu.Logic.Mediator.Queries.Notes;
using KiravRu.Logic.Mediator.QueryHandlers.Notes;
using KiravRu.Logic.Tests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace KiravRu.Logic.Tests.Mediator.QueryHandlers.Notes
{
    public class GetNoteForEditingByIdQueryHandlerTests
    {
        private readonly Mock<INoteRepository> _noteRepository;
        private readonly Mock<INoteAccessRepository> _noteAccessRepository;
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;

        public GetNoteForEditingByIdQueryHandlerTests()
        {
            _noteRepository = MockNoteRepository.GetMockNoteRepository();
            _noteAccessRepository = MockNoteAccessRepository.GetMockNoteAccessRepository();
            _roleRepository = MockRoleRepository.GetMockRoleRepository();
            _categoryRepository = MockCategoryRepository.GetMockCategoryRepository();
        }

        [Fact]
        public async Task testGetNoteForEditingById()
        {
            var handler = new GetNoteForEditingByIdQueryHandler(_noteRepository.Object, _noteAccessRepository.Object, _roleRepository.Object, _categoryRepository.Object);

            var request = new GetNoteForEditingByIdQuery()
            {
                NoteId = 1
            };
            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<GetNoteForEditingByIdQueryResult>();
            result.Article.Id.ShouldBe(1);
            result.Categories.Count.ShouldBe(2);
            result.Roles.AllRoles.Count.ShouldBe(3);
            result.Roles.UserRoles.Count.ShouldBe(2);
        }
    }
}
