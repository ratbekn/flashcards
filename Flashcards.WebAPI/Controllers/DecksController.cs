using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.Domain.Services.Cards;
using Flashcards.Domain.Services.Decks;
using Flashcards.WebAPI.Models;
using Flashcards.WebAPI.Models.Decks;
using Flashcards.WebAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly IDecksService decksService;
        private readonly ICardsService cardsService;

        public DecksController(IDecksService decksService, ICardsService cardsService)
        {
            this.decksService = decksService;
            this.cardsService = cardsService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeckModel), StatusCodes.Status201Created)]
        [ProducesResponseType( StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(DeckCreateModel deckCreateModel)
        {
            var userId = HttpContext.User.GetUserId();
            var newCards = await Task.WhenAll(deckCreateModel.Cards
                .Select(newCard => cardsService.CreateAsync(userId, newCard.Question, newCard.Answer)));

            var newDeck = await decksService.CreateAsync(userId, deckCreateModel.Name, newCards);

            return CreatedAtAction(nameof(Find), new { id = newDeck.Id }, new DeckModel
            {
                Id = newDeck.Id,
                Name = newDeck.Name,
                Cards = newCards
                    .ToList()
            });
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(DeckModel), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, DeckUpdateModel deckUpdateModel)
        {
            var deck = await decksService.FindAsync(id);

            if (deck == null)
                return NotFound(id);

            var userId = HttpContext.User.GetUserId();
            var cards = deckUpdateModel.Cards ?? new List<CardUpdateModel>();

            foreach (var card in cards.Where(card => card.Id.HasValue))
                await cardsService.UpdateOrDoNothingAsync(userId, card.Id.Value, card.Question, card.Answer);

            var newCards = await Task.WhenAll(cards.Where(card => card.Id == null)
                .Select(newCard => cardsService.CreateAsync(userId, newCard.Question, newCard.Answer)));

            deck = await decksService.GetAsync(id);

            if (deckUpdateModel.Name != null)
                deck.Name = deckUpdateModel.Name;

            deck.CardsIds.AddRange(newCards.Select(card => card.Id));
            deck.CardsIds.RemoveAll(cardId => deckUpdateModel.DeleteCards?.Contains(cardId) ?? false);

            return Ok(await decksService.UpdateAsync(deck.Id, deck));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeckModel), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Find(Guid id)
        {
            var deck = await decksService.FindAsync(id);

            if (deck == null)
                return NotFound(id);

            var cards = await Task.WhenAll(deck.CardsIds
                .Select(cardId => cardsService.GetAsync(cardId)));

            return Ok(new DeckModel
            {
                Id = deck.Id,
                Name = deck.Name,
                Cards = cards
                    .ToList()
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DeckModel>), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var decks = await decksService.GetUsersDecks(HttpContext.User.GetUserId());

            return Ok(await Task.WhenAll(decks.Select(async deck =>
            {
                var cards = await Task.WhenAll(deck.CardsIds
                    .Select(cardId => cardsService.GetAsync(cardId)));

                return new DeckModel
                {
                    Id = deck.Id,
                    Name = deck.Name,
                    Cards = cards
                        .ToList()
                };
            })));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType( StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deck = await decksService.FindAsync(id);

            if (deck == null)
                return NotFound(id);

            await cardsService.DeleteAsync(deck.CardsIds.ToArray());
            await decksService.DeleteAsync(deck.Id);

            return NoContent();
        }
    }
}