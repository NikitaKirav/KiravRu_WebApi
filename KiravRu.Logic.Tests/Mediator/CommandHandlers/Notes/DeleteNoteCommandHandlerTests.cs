using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Mediator.CommandHandlers.Notes;
using KiravRu.Logic.Mediator.Commands.Notes;
using KiravRu.Logic.Tests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace KiravRu.Logic.Tests.Mediator.CommandHandlers.Notes
{
    public class DeleteNoteCommandHandlerTests
    {
        private readonly Mock<INoteRepository> _noteRepository;

        public DeleteNoteCommandHandlerTests()
        {
            _noteRepository = MockNoteRepository.GetMockNoteRepository();
        }

        [Fact]
        public async Task testDeleteNote_WasDeleted()
        {
            var note = MockNoteRepository.GetTestArticles()[0];
            var handler = new DeleteNoteCommandHandler(_noteRepository.Object);

            var request = new DeleteNoteCommand() { NoteId = 1 };
            var result = await handler.Handle(request, CancellationToken.None);

            _noteRepository.Verify(x => x.Delete(note), Times.Once());
            result.ShouldBe(true);
        }
    }
}
