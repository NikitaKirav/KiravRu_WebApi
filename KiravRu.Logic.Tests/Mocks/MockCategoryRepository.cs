using KiravRu.Logic.Domain.Categories;
using KiravRu.Logic.Interface.Categories;
using Moq;
using System.Collections.Generic;
using System.Threading;

namespace KiravRu.Logic.Tests.Mocks
{
    public static class MockCategoryRepository
    {
        public static Mock<ICategoryRepository> GetMockCategoryRepository()
        {
            var mockRepo = new Mock<ICategoryRepository>();

            mockRepo
                .Setup(repo => repo.GetCategoriesAsync(CancellationToken.None))
                .ReturnsAsync(GetCategories());

            mockRepo
                .Setup(repo => repo.GetCategoryByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(GetCategories()[0]);

            mockRepo
                .Setup(repo => repo.OrderAllCategoryAsync(0, CancellationToken.None))
                .ReturnsAsync(GetCategories());


            return mockRepo;
        }

        private static List<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Frontend"
                },
                new Category()
                {
                    Id = 1,
                    Name = "Backend"
                }
            };
        }
    }
}
