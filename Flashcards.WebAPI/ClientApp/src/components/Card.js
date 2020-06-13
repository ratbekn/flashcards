import React, { Component } from 'react';
import "./Card.css"

export class Card extends Component {
    static displayName = Card.name;

    constructor(props) {
        super(props);
        this.state = { position: "notClicked", answerOrQuestion: Math.floor(Math.random() * 2) };
    }



    handleKnowClick = () => {
        this.setState({ position: "clickedKnow" });
    }

    handleNotKnowClick = () => {
        this.setState({ position: "clickedNotKnow" })
    }


    render() {
        const first = this.state.answerOrQuestion === 1 ? this.props.card.answer : this.props.card.question;
        const second = this.state.answerOrQuestion === 1 ? this.props.card.question : this.props.card.answer;
        if (this.state.position === "notClicked")
            return (
                <div className="card w-25 deck-card">
                    <div className="card-body">
                        <h5 className="card-title">{first}</h5>
                        <button className="btn btn-primary card-answer-button" onClick={this.handleKnowClick}>Знаю</button>
                        <button className="btn btn-primary" onClick={this.handleNotKnowClick}>Не знаю</button>
                    </div>
                </div>
            );
        if (this.state.position === "clickedKnow")
            return (
                <div className="card deck-card">
                    <div className="card-body">
                        <h5 className="card-title">{second}</h5>
                        <button className="btn btn-primary card-answer-button" onClick={this.props.onNextCard}>Следующая</button>
                        <button className="btn btn-primary" onClick={this.handleNotKnowClick}>Не знаю</button>
                    </div>
                </div>
            );
        if (this.state.position === "clickedNotKnow")
            return (
                <div className="card deck-card">
                    <div className="card-body">
                        <h5 className="card-title">{second}</h5>
                        <button className="btn btn-primary" onClick={this.props.onNextCard}>Следующая</button>
                    </div>
                </div>
            );
    }


}
