import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';
import { DeckEditor } from "./DeckEditor.js";
import { Deck } from './Deck.js';
import { Loader } from './Loader.js';

export class EditDeck extends Component {
  static displayName = EditDeck.name;

  constructor(props) {
    super(props);
    this.state = {
      name: "",
      cards: []
    };
  }

  componentDidMount()
  {
    this.populateDeck();
  }

  async populateDeck()
    {
        const token = await authService.getAccessToken();

        await fetch(`/api/decks/${this.props.match.params.id}`, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(response => response.json())
            .then(data => this.setState({ name: data.name, cards: data.cards }));
    }

  handleEditDeck = async (name, cards, deleteCards) => {
    const token = await authService.getAccessToken();
    await fetch(`/api/decks/${this.props.match.params.id}`, {
      method: 'PATCH',
      headers: {
        ...{
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
        ...(!token ? {} : { 'Authorization': `Bearer ${token}` })
      },
      body: JSON.stringify({
        name: name,
        cards: cards,
        deleteCards: deleteCards
      })
    })

    this.props.history.push('/')
  }

  render() {
    return (
      this.state.name ? <DeckEditor action={"Редактировать набор"} name={this.state.name} cards={this.state.cards} handleAction={this.handleEditDeck}></DeckEditor> 
      : <Loader></Loader>
    );
  }
}
