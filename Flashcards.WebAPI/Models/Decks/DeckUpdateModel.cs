using System;
using System.Collections.Generic;

namespace Flashcards.WebAPI.Models.Decks
{
    public class DeckUpdateModel
    {
        public string Name { get; set; }

        public List<CardCreateModel> NewCards { get; set; }

        public List<Guid> DeleteCards { get; set; }
    }
}