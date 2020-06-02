using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.Domain.Services.Cards;
using Flashcards.Domain.Services.Decks;
using Flashcards.WebAPI.Models.Decks;
using Flashcards.WebAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Flashcards.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly IDecksService decksService;
        private readonly ICardsService cardsService;
        private readonly ILogger<DecksController> logger;

        public DecksController(IDecksService decksService, ICardsService cardsService, ILogger<DecksController> logger)
        {
            this.decksService = decksService;
            this.cardsService = cardsService;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeckModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(DeckCreateModel deckCreateModel)
        {
            var userId = HttpContext.User.GetUserId();
            var newCards = await Task.WhenAll(deckCreateModel.Cards
                .Select(newCard => cardsService.CreateAsync(userId, newCard.Question, newCard.Answer)));

            var newDeck = await decksService.CreateAsync(userId, deckCreateModel.Name, newCards);

            return CreatedAtAction(nameof(GetAsync), new { id = newDeck.Id }, new DeckModel
            {
                Id = newDeck.Id,
                Name = newDeck.Name,
                Cards = newCards
                    .ToList()
            });
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

        [HttpGet]
        [ProducesResponseType(typeof(List<DeckModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
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
    }
}