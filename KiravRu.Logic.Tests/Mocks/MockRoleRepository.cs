using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Interface.Users;
using Moq;
using System.Collections.Generic;
using System.Threading;

namespace KiravRu.Logic.Tests.Mocks
{
    public static class MockRoleRepository
    {
        public static Mock<IRoleRepository> GetMockRoleRepository()
        {
            var roleNames = new List<string> { "admin" };
            var roleIds = new string[] { "role_id_1" };
            var mockRepo = new Mock<IRoleRepository>();

            mockRepo
                .Setup(repo => repo.GetRoleIdsByRoleNamesAsync(roleNames, CancellationToken.None))
                .ReturnsAsync(roleIds);

            mockRepo
                .Setup(repo => repo.GetRolesAsync(CancellationToken.None))
                .ReturnsAsync(GetRoles());

            return mockRepo;
        }

        private static List<Role> GetRoles()
        {
            return new List<Role>() {
                        new Role("admin"),
                        new Role("user"),
                        new Role("moderator") };
        }
    }
}
