import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);

    this.state = {
      decks: []
    }
  }

  componentDidMount() {
    fetch("/api/decks")
      .then(response => response.json())
      .then(data => this.setState({ decks: [...this.state.decks, ...data] }));
  }

  render() {
    const { decks } = this.state;

    return (
      <div class="card-deck">
          {
            decks.map(deck =>
              <div key={deck.id}>
                <p>Набор: {deck.name}</p>
                {
                    deck.cards.map(card =>
                      <div class="card text-white bg-dark mb-3">
                        <p>{card.question} : {card.answer}</p>
                      </div>
                    )
                }
              </div>)
          }
        <Link to="/add">Добавить набор</Link>
      </div>
    );
  }
}
