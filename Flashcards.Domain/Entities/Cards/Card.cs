using System;

namespace Flashcards.Domain.Entities.Cards
{
    public class Card
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}