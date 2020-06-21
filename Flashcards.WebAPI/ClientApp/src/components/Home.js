import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./Home.css";
import authService from './api-authorization/AuthorizeService'

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
  
  delete = async () =>
  {
    const token = await authService.getAccessToken();

    await fetch(`/api/decks/${this.state.aboutToDelete}`, {
            method: 'DELETE',
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });

    this.setState({aboutToDelete: null});
    
  }
  
  cancelDelete = () => {
    this.setState({aboutToDelete: null});
  }

  render() {
    const { decks } = this.state;
    if (this.state.aboutToDelete !== null)
    return (  
      <div>
      <button type="button" onClick={this.delete} className="btn btn-primary card-answer-button">Хочу удалить</button>
      <button type="button" onClick={this.cancelDelete} className="btn btn-primary">Не хочу удалять</button></div>)
    /* <div className="modal fade" id="exampleModal" tabIndex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div className="modal-dialog" role="document">
      <div className="modal-content">
        <div className="modal-header">
          <h5 className="modal-title" id="exampleModalLabel">Modal title</h5>
          <button type="button" className="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div className="modal-body">
          Жопа
        </div>
        <div className="modal-footer">
        */
          
        /* </div>
      </div>
    </div>
  </div>
  */
    
    return (
      <div className="card-deck container">
        {
          decks.map(deck =>
            <div key={deck.id} className="col-md-4 col-sm-12 card-container">
              <div className="card">
                <div className="card-body">
                <Link to={`/deck/${deck.id}`}><h5 className="card-title">{deck.name}</h5></Link>
                  <p className="card-text">{getCardText(deck)}</p>
                  <Link to={`/edit/${deck.id}`}>Редактировать </Link>
                  <a href="#" onClick={() => this.makeDeckAboutToDelete(deck.id)} className="card-link"> Удалить</a> 
                </div>
              </div>
            </div>) // TODO: найти как застилизовать кнопку под ссылку, чтобы не писать ахреф
        }
        {this.state.decksLoaded &&
          <div className="col-md-3 col-sm-12 card-container">
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">
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
    this.setState({aboutToDelete: id});
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
