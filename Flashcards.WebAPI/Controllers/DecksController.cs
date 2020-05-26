using System;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.Domain.Services.Cards;
using Flashcards.Domain.Services.Decks;
using Flashcards.WebAPI.Models.Decks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Flashcards.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly IDecksService decksService;
        private readonly ICardsService cardsService;
        private readonly IActionContextAccessor actionContextAccessor;

        public DecksController(IDecksService decksService, ICardsService cardsService, IActionContextAccessor actionContextAccessor)
        {
            this.decksService = decksService;
            this.cardsService = cardsService;
            this.actionContextAccessor = actionContextAccessor;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeckModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(DeckCreateModel deckCreateModel)
        {
            var newCards = await Task.WhenAll(deckCreateModel.Cards
                .Select(newCard => cardsService.CreateAsync(newCard.Question, newCard.Answer)));

            var newDeck = await decksService.CreateAsync(deckCreateModel.Name, newCards);

            return CreatedAtAction(nameof(GetAsync), new { id = newDeck.Id }, newDeck);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeckModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var deck = await decksService.GetAsync(id);
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
    }
}