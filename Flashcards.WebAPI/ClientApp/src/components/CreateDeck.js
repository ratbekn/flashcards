import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { DeckEditor } from './DeckEditor';

export class CreateDeck extends Component {
  static displayName = CreateDeck.name;

  constructor(props) {
    super(props);
    this.state = {
      name: "",
      cards: []
    };
  }

  handleCreateDeck = async (name, cards) => {
    const token = await authService.getAccessToken();
    await fetch('/api/decks', {
      method: 'POST',
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
      })
    })

    this.props.history.push('/')
  }

  render() {
    return (
      <DeckEditor action={"Создать набор"} name={this.state.name} cards={this.state.cards} handleAction={this.handleCreateDeck}></DeckEditor>
    );
  }
}
