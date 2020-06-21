using System;
using System.Collections.Generic;

namespace Flashcards.WebAPI.Models.Decks
{
    public class DeckUpdateModel
    {
        public string Name { get; set; }

        public List<CardUpdateModel> Cards { get; set; }

        public List<Guid> DeleteCards { get; set; }
    }
}