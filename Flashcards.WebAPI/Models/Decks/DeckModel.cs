using System;
using System.Collections.Generic;
using Flashcards.Domain.Entities.Cards;

namespace Flashcards.WebAPI.Models.Decks
{
    public class DeckModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
    }
}