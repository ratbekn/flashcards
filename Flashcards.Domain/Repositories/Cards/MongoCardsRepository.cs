using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.Domain.Entities.Cards;
using MongoDB.Driver;

namespace Flashcards.Domain.Repositories.Cards
{
    public class MongoCardsRepository : ICardsRepository
    {
        private readonly IMongoCollection<Card> cardCollection;
        private const string CollectionName = "cards";

        public MongoCardsRepository(IMongoDatabase db)
        {
            cardCollection = db.GetCollection<Card>(CollectionName);
            cardCollection.Indexes.CreateOne(new CreateIndexModel<Card>(
                Builders<Card>.IndexKeys.Hashed(card => card.UserId)));
        }

        public async Task<Guid> AddAsync(Card card)
        {
            await cardCollection.InsertOneAsync(card);

            return (await FindAsync(card.Id))?.Id ?? throw new Exception($"Insert card {card.Id} fail");
        }

        public async Task<Card> GetAsync(Guid id) => await FindAsync(id) ?? throw new Exception($"Not found card {id}");

        public async Task<IEnumerable<Card>> GetUsersCards(Guid userId)
        {
            return (await cardCollection.FindAsync(card => card.UserId == userId))
                .ToEnumerable();
        }

        public async Task DeleteAsync(params Guid[] deleteCardsIds)
        {
            await cardCollection.DeleteManyAsync(card => deleteCardsIds.Contains(card.Id));
        }

        private async Task<Card> FindAsync(Guid cardId)
        {
            return await (await cardCollection.FindAsync(card => card.Id == cardId))
                .FirstOrDefaultAsync();
        }
    }
}