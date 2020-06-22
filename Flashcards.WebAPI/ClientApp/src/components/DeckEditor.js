import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';
import "./Home.css";
import { Formik } from "formik";

export class DeckEditor extends Component {
  static displayName = DeckEditor.name;

  constructor(props) {
    super(props);
    this.state = {
      name: this.props.name,
      cards: this.props.cards,
      cardsToDelete: []
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
    const currentCard = this.state.cards[i]
    this.state.cards[i] = {
      question: e.target.value,
      answer: currentCard.answer
    }

    this.setState({
      name: this.state.name,
      cards: this.state.cards,
    })
  }

  handleAnswer(e, i) {
    const currentCard = this.state.cards[i]
    this.state.cards[i] = {
      question: currentCard.question,
      answer: e.target.value
    }

    this.setState({
      name: this.state.name,
      cards: this.state.cards
    })
  }

  deleteCard = (index) => {
      const cards = this.state.cards;
      if (cards[index].id)
        this.state.cardsToDelete.push(cards[index].id);
      this.setState({cards: cards.filter((el, i) => i != index)});
  }


  render() {
    
    return (
      <div>
        <div className="input-group input-group-lg">
        <input className="form-control no-border" placeholder="Введите название набора" onChange={(e) => this.handleTitle(e)} value={this.state.name} />
        </div>
        
      <div className="card-deck container">
        {
          this.state.cards.map((card, i) => {
            return (
              <div key={i} className="col-md-4 col-sm-12 card-container">
                <div className="card">
                  <div className="card-body">
                    <div className="card-text">
                    <label>Вопрос<input className="form-control" onChange={(e) => this.handleQuestion(e, i)} value={card.question} /></label>
                    
                <label>Ответ<input className="form-control" onChange={(e) => this.handleAnswer(e, i)} value={card.answer} /></label>
                </div>
              </div>
              
              </div>
              <button className="btn btn-link" onClick={() => this.deleteCard(i)}>Удалить</button>
              </div>
            )
          })
        }
        

        <div className="col-md-4 col-sm-12 card-container">
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">
                <button className="btn btn-link" onClick={(e) => this.addCard(e)}>Добавить карточку</button>
                </h5>
                
              </div>
            </div>
          </div>
        </div>

        <br />
        <br />
        <button className="btn btn-primary" onClick={() => this.props.handleAction(this.state.name, this.state.cards, this.state.cardsToDelete)}>{this.props.action}</button>
      </div>
    );
  }
}
