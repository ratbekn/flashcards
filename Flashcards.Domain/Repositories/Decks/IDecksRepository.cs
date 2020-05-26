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
        Task<List<Deck>> GetAllAsync();
    }
}