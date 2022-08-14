using System.Collections.Generic;

namespace KiravRu.Controllers.v1.Model
{
    public class SaveNoteRequestModel
    {
        public NoteRequestModel Article { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }

    public class NoteRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public string DateCreate { get; set; }
        public string DateChange { get; set; }
        public string Text { get; set; }
        public string ShortDescription { get; set; }
        public int CategoryId { get; set; }
        public int DifficultyLevel { get; set; }
        public bool IsFavorite { get; set; }
        public bool Visible { get; set; }
    }
}
