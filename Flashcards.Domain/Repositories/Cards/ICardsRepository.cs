using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;

namespace Flashcards.Domain.Repositories.Cards
{
    public interface ICardsRepository
    {
        Task<Guid> AddAsync(Card card);
        Task<Card> GetAsync(Guid id);
        Task<Card> FindAsync(Guid id);
        Task UpdateAsync(Card card);
        Task<IEnumerable<Card>> GetCards();
        Task DeleteAsync(params Guid[] deleteCardsIds);
    }
}
