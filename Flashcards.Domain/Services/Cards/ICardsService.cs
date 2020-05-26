using System;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;

namespace Flashcards.Domain.Services.Cards
{
    public interface ICardsService
    {
        Task<Card> CreateAsync(string question, string answer);
        Task<Card> GetAsync(Guid id);
    }
}