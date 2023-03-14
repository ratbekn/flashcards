using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Decks;

namespace Flashcards.Domain.Repositories.Decks
{
    public interface IDecksRepository
    {
        Task<Guid> AddAsync(Deck deck);
        Task<Deck> GetAsync(Guid id);
        Task<Deck> FindAsync(Guid id);
        Task<IEnumerable<Deck>> GetDecks();
        Task<Deck> UpdateAsync(Guid deckId, Deck newDeck);
        Task DeleteAsync(Guid deckId);
    }
}
