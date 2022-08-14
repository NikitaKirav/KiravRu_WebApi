using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Interface.Notes;
using Moq;
using System.Collections.Generic;
using System.Threading;

namespace KiravRu.Logic.Tests.Mocks
{
    public static class MockNoteAccessRepository
    {
        public static Mock<INoteAccessRepository> GetMockNoteAccessRepository()
        {
            var rolesIdLast = new string[] { "role_id_1" };
            var mockRepo = new Mock<INoteAccessRepository>();

            mockRepo
                .Setup(repo => repo.GetRolesAsync(1, CancellationToken.None))
                .ReturnsAsync(new List<string>() { "admin", "user" });

            mockRepo
                .Setup(repo => repo.GetListOfNotesByRoleIds(rolesIdLast, CancellationToken.None))
                .ReturnsAsync(GetNoteAccessTest());

            return mockRepo;
        }

        public static List<NoteAccess> GetNoteAccessTest()
        {
            return new List<NoteAccess>
            {
                new NoteAccess()
                {
                    Id = 1,
                    RoleId = "role_id_1",
                    NoteId = 1
                },
                new NoteAccess()
                {
                    Id = 3,
                    RoleId = "role_id_1",
                    NoteId = 2
                }
            };
        }
    }
}
