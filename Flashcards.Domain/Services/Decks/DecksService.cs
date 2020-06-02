using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;
using Flashcards.Domain.Entities.Decks;
using Flashcards.Domain.Repositories.Decks;

namespace Flashcards.Domain.Services.Decks
{
    public class DecksService : IDecksService
    {
        private readonly IDecksRepository repository;

        public DecksService(IDecksRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Deck> CreateAsync(Guid userId, string name, IEnumerable<Card> cards)
        {
            var newDeck = new Deck
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name,
                CardsIds = cards
                    .Select(card => card.Id)
                    .ToList()
            };

            await repository.AddAsync(newDeck);

            return newDeck;
        }

        public async Task<Deck> GetAsync(Guid id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<List<Deck>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<List<Deck>> GetUsersDecks(Guid userId)
        {
            return (await repository.GetUsersDecks(userId))
                .ToList();
        }
    }
}