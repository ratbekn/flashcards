using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;

namespace Flashcards.Domain.Services.Cards
{
    public interface ICardsService
    {
        Task<Card> CreateAsync(string question, string answer);
        Task UpdateOrDoNothingAsync(Guid cardId, string question, string answer);
        Task<Card> GetAsync(Guid id);
        Task<IEnumerable<Card>> GetCards();
        Task DeleteAsync(params Guid[] deleteCardsIds);
    }
}
