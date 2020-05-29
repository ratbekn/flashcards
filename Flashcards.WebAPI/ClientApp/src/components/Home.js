import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./Home.css";

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
      <div className="card-deck container">
        {
          decks.map(deck =>
            <div key={deck.id} className="col-md-3 col-sm-12 card-container">
              <div className="card">
                <div className="card-body">
                  <h5 className="card-title">{deck.name}</h5>
                  <p className="card-text">{getCardText(deck)}</p>
                </div>
              </div>
            </div>)
        }
      </div>
    );
  }
}

const getCardText = (deck) => {
  const cards = deck.cards;
  if (!cards) 
    return;
  const firstThree = cards.slice(0, 3).map(c => c.question).join(', ');
  return cards.length > 3 ? firstThree + "..." : firstThree;
}
