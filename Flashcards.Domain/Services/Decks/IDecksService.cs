﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;
using Flashcards.Domain.Entities.Decks;

namespace Flashcards.Domain.Services.Decks
{
    public interface IDecksService
    {
        Task<Deck> CreateAsync(string name, IEnumerable<Card> cards);
        Task<Deck> UpdateAsync(Guid deckId, Deck newDeck);
        Task<Deck> GetAsync(Guid id);
        Task<Deck> FindAsync(Guid id);
        Task<List<Deck>> GetDecks();
        Task DeleteAsync(Guid deckId);
    }
}
