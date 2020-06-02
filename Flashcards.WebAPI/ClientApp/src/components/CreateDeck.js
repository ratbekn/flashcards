import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class CreateDeck extends Component {
  static displayName = CreateDeck.name;

  constructor(props) {
    super(props);
    this.state = {
      name: "",
      cards: []
    };
  }

  addCard() {
    this.setState({
      cards: [...this.state.cards, ""]
    });
  }

  handleTitle(e) {
    this.state.name = e.target.value

    this.setState({
      name: this.state.name,
      cards: this.state.cards
    })
  }

  handleQuestion(e, i) {
    let currentCard = this.state.cards[i]
    this.state.cards[i] = {
      question: e.target.value,
      answer: currentCard.answer
    }

    this.setState({
      name: this.state.name,
      cards: this.state.cards
    })
  }

  handleAnswer(e, i) {
    let currentCard = this.state.cards[i]
    this.state.cards[i] = {
      question: currentCard.question,
      answer: e.target.value
    }

    this.setState({
      name: this.state.name,
      cards: this.state.cards
    })
  }

  async handleCreateDeck() {
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
        name: this.state.name,
        cards: this.state.cards,
      })
    })

    this.props.history.push('/')
  }

  render() {
    return (
      <div>
        <h1>Название набора: </h1>
        <input onChange={(e) => this.handleTitle(e)} />

        {
          this.state.cards.map((card, i) => {
            return (
              <div key={i}>
                <hr />
                <p>Вопрос: </p>
                <input onChange={(e) => this.handleQuestion(e, i)} value={card.question} />
                <p>Ответ: </p>
                <input onChange={(e) => this.handleAnswer(e, i)} value={card.answer} />
              </div>
            )
          })
        }

        <hr />
        <button className="btn btn-primary" onClick={(e) => this.addCard(e)}>Добавить карточку</button>

        <br />
        <br />
        <button className="btn btn-primary" onClick={() => this.handleCreateDeck()}>Создать набор</button>
      </div>
    );
  }
}
