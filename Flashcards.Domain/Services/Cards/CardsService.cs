using System;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;
using Flashcards.Domain.Repositories;

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

        public Task<Card> GetAsync(Guid id) => repository.GetAsync(id);
    }
}