using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Decks;

namespace Flashcards.Domain.Repositories.Decks
{
    public class InMemoryDecksRepository : IDecksRepository
    {
        private readonly Dictionary<Guid, Deck> decks = new Dictionary<Guid, Deck>();

        public Task<Guid> AddAsync(Deck deck)
        {
            decks.Add(deck.Id, deck);

            return Task.FromResult(deck.Id);
        }

        public Task<Deck> GetAsync(Guid id) => Task.FromResult(decks[id]);
        public Task<List<Deck>> GetAllAsync()
        {
            return Task.FromResult(decks.Values.ToList());
        }

        public Task<IEnumerable<Deck>> GetUsersDecks(Guid userId) => Task.FromResult(decks.Values.Where(deck => deck.UserId == userId));
    }
}