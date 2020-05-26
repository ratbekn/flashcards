using System;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;

namespace Flashcards.Domain.Repositories
{
    public interface ICardsRepository
    {
        Task<Guid> AddAsync(Card card);
        Task<Card> GetAsync(Guid id);
    }
}