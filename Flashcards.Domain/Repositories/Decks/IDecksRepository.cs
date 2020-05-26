using System;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Decks;

namespace Flashcards.Domain.Repositories.Decks
{
    public interface IDecksRepository
    {
        Task<Guid> AddAsync(Deck deck);
        Task<Deck> GetAsync(Guid id);
    }
}