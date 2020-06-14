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

        public Task<Deck> UpdateAsync(Guid deckId, Deck newDeck) => repository.UpdateAsync(deckId, newDeck);

        public async Task<Deck> GetAsync(Guid id) => await repository.GetAsync(id);

        public async Task<Deck> FindAsync(Guid id) => await repository.FindAsync(id);

        public async Task<List<Deck>> GetUsersDecks(Guid userId)
        {
            return (await repository.GetUsersDecks(userId))
                .ToList();
        }

        public async Task DeleteAsync(Guid deckId)
        {
            await repository.DeleteAsync(deckId);
        }
    }
}