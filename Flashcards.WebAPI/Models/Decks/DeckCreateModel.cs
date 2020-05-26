using System.Collections.Generic;

namespace Flashcards.WebAPI.Models.Decks
{
    public class DeckCreateModel
    {
        public string Name { get; set; }
        public List<CardCreateModel> Cards { get; set; }
    }
}