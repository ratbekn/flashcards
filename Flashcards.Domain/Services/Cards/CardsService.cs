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

        public async Task<Card> CreateAsync(Guid userId, string question, string answer)
        {
            var newCard = new Card
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Question = question,
                Answer = answer
            };

            await repository.AddAsync(newCard);

            return newCard;
        }

        public async Task UpdateOrDoNothingAsync(Guid userId, Guid cardId, string question, string answer)
        {
            var card = await repository.FindAsync(cardId);

            if (card == null)
                return;

            await repository.UpdateAsync(new Card
            {
                Id = cardId,
                UserId = userId,
                Question = question,
                Answer = answer
            });
        }

        public Task<Card> GetAsync(Guid id) => repository.GetAsync(id);
        public Task<IEnumerable<Card>> GetUsersCards(Guid userId) => repository.GetUsersCards(userId);

        public async Task DeleteAsync(params Guid[] deleteCardsIds)
        {
            await repository.DeleteAsync(deleteCardsIds);
        }
    }
}