using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Decks;
using MongoDB.Driver;

namespace Flashcards.Domain.Repositories.Decks
{
    public class MongoDecksRepository : IDecksRepository
    {
        private readonly IMongoCollection<Deck> deckCollection;
        private const string CollectionName = "decks";

        public MongoDecksRepository(IMongoDatabase db)
        {
            deckCollection = db.GetCollection<Deck>(CollectionName);
            deckCollection.Indexes.CreateOne(new CreateIndexModel<Deck>(
                Builders<Deck>.IndexKeys.Hashed(deck => deck.UserId)));
        }

        public async Task<Guid> AddAsync(Deck deck)
        {
            await deckCollection.InsertOneAsync(deck);

            return (await FindInternalAsync(deck.Id))?.Id ?? throw new Exception($"Insert deck {deck.Id} fail");
        }

        public async Task<Deck> GetAsync(Guid id) => await FindInternalAsync(id) ?? throw new Exception($"Not found deck {id}");
        public async Task<Deck> FindAsync(Guid id) => await FindInternalAsync(id);

        public async Task<IEnumerable<Deck>> GetUsersDecks(Guid userId)
        {
            return (await deckCollection.FindAsync(card => card.UserId == userId))
                .ToEnumerable();
        }

        public async Task<Deck> UpdateAsync(Guid deckId, Deck newDeck)
        {
            var result = await deckCollection.ReplaceOneAsync(deck => deck.Id == deckId, newDeck);

            if (result.IsAcknowledged)
                return await FindInternalAsync(deckId);

            throw new Exception($"Update deck {deckId} fail");
        }

        private async Task<Deck> FindInternalAsync(Guid deckId)
        {
            return await (await deckCollection.FindAsync(deck => deck.Id == deckId))
                .FirstOrDefaultAsync();
        }
    }
}