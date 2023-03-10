import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { DeckEditor } from './DeckEditor';
import { createDeck } from './api';

export class CreateDeck extends Component {

  constructor(props) {
    super(props);
    this.state = {
      name: "",
      cards: []
    };
  }

  handleCreateDeck = async (name, cards) => {
    try {
      await createDeck(name, cards)
    }
    catch (e) {
      alert("Произошла ошибка, попробуйте ещё раз");
    }

    this.props.history.push('/');

  }

  render() {
    return (
      <DeckEditor action={"Создать набор"} name={this.state.name} cards={this.state.cards} handleAction={this.handleCreateDeck} />
    );
  }
}
