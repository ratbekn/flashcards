import React, { Component } from 'react';
import shuffle from 'lodash/shuffle';
import { Card } from "./Card.js";
import authService from "./api-authorization/AuthorizeService";

export class Deck extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cards: [],
            indexShownCard: 0
        };
    }

    componentDidMount() {
        this.populateDeck();
    }

    render() {
        const { cards } = this.state;
        if (this.state.cards.length === 0)
            return (<div className="d-flex justify-content-center">
                <div className="spinner-border" role="status">
                    <span className="sr-only">Loading...</span>
                </div>
            </div>);
        if (this.state.indexShownCard >= this.state.cards.length)
            return (<b>Карточки кончились</b>);
        const card = cards[this.state.indexShownCard];
        return (
            <Card card={card} onNextCard={this.onNextCard} key={card.id} />
        );
    }

    onNextCard = (position) => {
        if (position == "clickedNotKnow")
        {
            this.state.cards.push(this.state.cards[this.state.indexShownCard]);
        }
        this.setState({ indexShownCard: this.state.indexShownCard + 1 });
    }

    async populateDeck()
    {
        const token = await authService.getAccessToken();

        await fetch(`/api/decks/${this.props.match.params.id}`, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(response => response.json())
            .then(data => this.setState({ cards: shuffle(data.cards) }));

    }

}
