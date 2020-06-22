import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./Home.css";
import authService from './api-authorization/AuthorizeService';
import { ModalClass } from "./ModalClass.js";

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = {
      decks: [],
      decksLoaded: false,
      aboutToDelete: null
    };

  }

  componentDidMount() {
    this.populateDecks();
  }

  delete = async () => {
    const token = await authService.getAccessToken();

    await fetch(`/api/decks/${this.state.aboutToDelete}`, {
      method: 'DELETE',
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });

    this.setState({ aboutToDelete: null });
    this.populateDecks();

  }

  cancelDelete = () => {
    this.setState({ aboutToDelete: null });
  }

  render() {
    const { decks } = this.state;
    return (

      <div className="card-deck">
        {this.state.aboutToDelete != null && <ModalClass title={"Удаление набора"} cancelButton={"Отмена"} successButton={"Удалить"} cancel={this.cancelDelete} success={this.delete}>Вы действительно хотите удалить набор?</ModalClass>}
        {

          decks.map(deck =>
            <div key={deck.id} className="col-md-4 col-sm-12 card-container">
              <div className="card">
                <div className="card-body">
                  <Link to={`/deck/${deck.id}`}><h5 className="card-title">{deck.name}</h5></Link>
                  <p className="card-text">{getCardText(deck)}</p>
                  <Link to={`/edit/${deck.id}`} className="card-link">Редактировать </Link>
                  <a href="#" className="card-link" onClick={() => this.makeDeckAboutToDelete(deck.id)}>Удалить</a>
                </div>

              </div>
            </div>)
        }
        {this.state.decksLoaded &&
          <div className="col-md-4 col-sm-12 card-container">
            <div className="card">
              <div className="card-body">
                <h5 className="card-text">
                  <Link to="/add">Добавить набор</Link>
                </h5>
              </div>
            </div>
          </div>
        }
      </div>
    );
  }

  makeDeckAboutToDelete = (id) => {
    this.setState({ aboutToDelete: id });
  }

  async populateDecks() {
    const token = await authService.getAccessToken();

    await fetch("/api/decks", {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    })
      .then(response => response.json())
      .then(data => this.setState({ decks: data, decksLoaded: true }));
  }
}

const getCardText = (deck) => {
  const cards = deck.cards;
  if (!cards)
    return;
  const firstThree = cards.slice(0, 3).map(c => c.question).join(', ');
  return cards.length > 3 ? firstThree + "..." : firstThree;
}
