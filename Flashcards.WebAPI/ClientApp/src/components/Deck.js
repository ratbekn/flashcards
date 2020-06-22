import React, { Component } from 'react';
import shuffle from 'lodash/shuffle';
import { Card } from "./Card.js";
import { Loader } from "./Loader.js"
import { getDeck } from './api.js';

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

        if (cards.length === 0)
            return <Loader></Loader>;
        console.log(cards);
        if (this.state.indexShownCard >= cards.length && cards.length != 0)
            return (<b>Карточки кончились</b>);
        const card = cards[this.state.indexShownCard];
        return (
            <Card card={card} onNextCard={this.onNextCard} key={card.id} />
        );
    }

    onNextCard = (position) => {
        if (position == "clickedNotKnow") {
            this.state.cards.push(this.state.cards[this.state.indexShownCard]);
        }
        this.setState({ indexShownCard: this.state.indexShownCard + 1 });
    }

    async populateDeck() {
        try {
            const data = await getDeck(this.props.match.params.id);
            this.setState({ cards: shuffle(data.cards) });
        }
            
        catch(e)
        {
            alert("Произошла ошибка, попробуйте ещё раз");
        }
    }

}
