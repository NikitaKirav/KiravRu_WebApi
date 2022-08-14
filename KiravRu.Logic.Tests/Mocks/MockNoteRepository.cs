using KiravRu.Logic.Domain;
using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Interface.Notes;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KiravRu.Logic.Tests.Mocks
{
    public static class MockNoteRepository
    {
        public static Mock<INoteRepository> GetMockNoteRepository()
        {
            var mockRepo = new Mock<INoteRepository>();

            mockRepo
                .Setup(repo => repo.GetNotesAsync(CancellationToken.None))
                .ReturnsAsync(GetTestArticles());

            mockRepo
                .Setup(repo => repo.GetNoteByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(GetTestArticles()[0]);

            mockRepo
                .Setup(repo => repo.GetNotesWithFilter(new NoteFilter(), MockNoteAccessRepository.GetNoteAccessTest()))
                .Returns(GetPageList());

            mockRepo
                .Setup(repo => repo.GetNoteByIdWithAccess(1, MockNoteAccessRepository.GetNoteAccessTest()))
                .Returns(GetTestArticles()[0]);

            return mockRepo;
        }

        private static PageList<Note> GetPageList()
        {
            return new PageList<Note>(GetTestArticles(), 3);
        }

        public static List<Note> GetTestArticles()
        {
            return new List<Note>()
            {
                new Note() {
                    Id = 1,
                    CategoryId = 2,
                    DateChange = new DateTime(2015, 7, 20),
                    DateCreate = new DateTime(2015, 7, 19),
                    DifficultyLevel = 3,
                    ImagePath = "",
                    ImageText = "",
                    IsFavorite = false,
                    Name = "React - How use hooks",
                    Number = 1,
                    ShortDescription = "Here, you can find a short instruction",
                    Text = "React Text",
                    Visible = true
                },
                new Note() {
                    Id = 2,
                    CategoryId = 2,
                    DateChange = new DateTime(2015, 8, 20),
                    DateCreate = new DateTime(2015, 8, 19),
                    DifficultyLevel = 5,
                    ImagePath = "",
                    ImageText = "",
                    IsFavorite = false,
                    Name = "ASP.NET - Working with repositories",
                    Number = 1,
                    ShortDescription = "Here, you can find a short instruction",
                    Text = "ASP.NET Text",
                    Visible = true
                },
            };
        }
    }
}