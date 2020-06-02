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
        Task<IEnumerable<Card>> GetUsersCards(Guid userId);
    }
}