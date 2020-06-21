using System;

namespace Flashcards.WebAPI.Models
{
    public class CardUpdateModel
    {
        public Guid? Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}