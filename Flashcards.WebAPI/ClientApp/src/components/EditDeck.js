import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';
import { DeckEditor } from "./DeckEditor.js";
import { Deck } from './Deck.js';
import { Loader } from './Loader.js';
import { editDeck } from './api';
import { getDeck } from './api';

export class EditDeck extends Component {
  static displayName = EditDeck.name;

  constructor(props) {
    super(props);
    this.state = {
      name: "",
      cards: []
    };
  }

  componentDidMount() {
    this.populateDeck();
  }

  async populateDeck() {
    try {
      const data = await getDeck(this.props.match.params.id);
      this.setState({ name: data.name, cards: data.cards });
    }
    catch (e) {
      alert("Произошла ошибка, попробуйте ещё раз");
    }
}

  handleEditDeck = (name, cards, deleteCards) => {
    editDeck(this.props.match.params.id, name, cards, deleteCards)
      .then(() => this.props.history.push('/'))
      .catch((e) => alert("Произошла ошибка, попробуйте ещё раз"));
  }

  render() {
    return (
      this.state.name ? <DeckEditor action={"Редактировать набор"} name={this.state.name} cards={this.state.cards} handleAction={this.handleEditDeck}></DeckEditor>
        : <Loader></Loader>
    );
  }
}
