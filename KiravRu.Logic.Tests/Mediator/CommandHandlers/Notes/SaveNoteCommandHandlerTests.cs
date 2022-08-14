using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Mediator.CommandHandlers.Notes;
using KiravRu.Logic.Mediator.Commands.Notes;
using KiravRu.Logic.Tests.Mocks;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace KiravRu.Logic.Tests.Mediator.CommandHandlers.Notes
{
    public class SaveNoteCommandHandlerTests
    {
        private readonly Mock<INoteRepository> _noteRepository;
        private readonly Mock<INoteAccessRepository> _noteAccessRepository;

        public SaveNoteCommandHandlerTests()
        {
            _noteRepository = MockNoteRepository.GetMockNoteRepository();
            _noteAccessRepository = MockNoteAccessRepository.GetMockNoteAccessRepository();
        }

        [Fact]
        public async Task testGetNoteById_AddedSuccess()
        {
            var note = MockNoteRepository.GetTestArticles()[0];
            note.Id = 0;
            var handler = new SaveNoteCommandHandler(_noteRepository.Object, _noteAccessRepository.Object);

            var request = new SaveNoteCommand() { 
                Note = note,
                Roles = new List<string> { "admin", "user" }
            };
            var result = await handler.Handle(request, CancellationToken.None);

            _noteRepository.Verify(x => x.AddToSet(note), Times.Once());
            result.ShouldBeOfType<int>();
            result.ShouldBe(0);
        }

        [Fact]
        public async Task testGetNoteById_UpdatedSuccess()
        {
            var note = MockNoteRepository.GetTestArticles()[0];
            var handler = new SaveNoteCommandHandler(_noteRepository.Object, _noteAccessRepository.Object);

            var request = new SaveNoteCommand()
            {
                Note = note,
                Roles = new List<string> { "admin", "user" }
            };
            var result = await handler.Handle(request, CancellationToken.None);

            _noteRepository.Verify(x => x.Update(note), Times.Once());
            result.ShouldBeOfType<int>();
            result.ShouldBe(1);
        }
    }
}
