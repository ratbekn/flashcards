using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;

namespace Flashcards.Domain.Repositories.Cards
{
    public class InMemoryCardsRepository : ICardsRepository
    {
        private readonly Dictionary<Guid, Card> cards = new Dictionary<Guid, Card>();

        public Task<Guid> AddAsync(Card card)
        {
            cards.Add(card.Id, card);

            return Task.FromResult(card.Id);
        }

        public Task<Card> GetAsync(Guid id) => Task.FromResult(cards[id]);
        public Task<IEnumerable<Card>> GetUsersCards(Guid userId)
        {
            return Task.FromResult(cards.Values.Where(card => card.UserId == userId));
        }
    }
}