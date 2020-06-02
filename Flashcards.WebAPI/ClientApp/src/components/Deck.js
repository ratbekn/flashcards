import React, { Component } from 'react';
import shuffle from 'lodash/shuffle';
import { Card } from "./Card.js";

export class Deck extends Component {
    constructor(props) {
        super(props);
        this.state = { 
            cards: [], 
            indexShownedCard: 0 
        };
    }
    
    componentDidMount() {
        fetch(`/api/decks/${this.props.match.params.id}`)
            .then(response => response.json())
            .then(data => this.setState({cards: shuffle(data.cards)}));
    }

    render()
    {
        const { cards } = this.state;
        return (
            <Card card={cards[this.state.indexShownedCard]} /> 
            );
    }

}
