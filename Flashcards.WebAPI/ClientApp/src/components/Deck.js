import React, { Component } from 'react';
import shuffle from 'lodash/shuffle';
import { Card } from "./Card.js";

export class Deck extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cards: [],
            indexShownCard: 0
        };
    }

    componentDidMount() {
        fetch(`/api/decks/${this.props.match.params.id}`)
            .then(response => response.json())
            .then(data => this.setState({ cards: shuffle(data.cards) }));
    }

    render() {
        const { cards } = this.state;
        if (this.state.cards.length == 0)
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

    onNextCard = () => {
        this.setState({ indexShownCard: this.state.indexShownCard + 1 });
    }

}
