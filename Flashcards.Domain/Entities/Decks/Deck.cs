using System;
using System.Collections.Generic;

namespace Flashcards.Domain.Entities.Decks
{
    public class Deck
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<Guid> CardsIds { get; set; }
    }
}