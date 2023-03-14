using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;
using Flashcards.Domain.Repositories.Cards;

namespace Flashcards.Domain.Services.Cards
{
    public class CardsService : ICardsService
    {
        private readonly ICardsRepository repository;

        public CardsService(ICardsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Card> CreateAsync(string question, string answer)
        {
            var newCard = new Card
            {
                Id = Guid.NewGuid(),
                Question = question,
                Answer = answer
            };

            await repository.AddAsync(newCard);

            return newCard;
        }

        public async Task UpdateOrDoNothingAsync(Guid cardId, string question, string answer)
        {
            var card = await repository.FindAsync(cardId);

            if (card == null)
                return;

            await repository.UpdateAsync(new Card
            {
                Id = cardId,
                Question = question,
                Answer = answer
            });
        }

        public Task<Card> GetAsync(Guid id) => repository.GetAsync(id);
        public Task<IEnumerable<Card>> GetCards() => repository.GetCards();

        public async Task DeleteAsync(params Guid[] deleteCardsIds)
        {
            await repository.DeleteAsync(deleteCardsIds);
        }
    }
}
