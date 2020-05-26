using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;
using Flashcards.Domain.Entities.Decks;

namespace Flashcards.Domain.Services.Decks
{
    public interface IDecksService
    {
        Task<Deck> CreateAsync(string name, IEnumerable<Card> cards);
        Task<Deck> GetAsync(Guid id);
        Task<List<Deck>> GetAllAsync();
    }
}