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
        this.setState({ position: "clickedNotKnow" });
        console.log(this.state.position);
    }

    handleNextCard = (position) => {
        this.props.onNextCard(position);
    }

    render() {
        const first = this.state.answerOrQuestion === 1 ? this.props.card.answer : this.props.card.question;
        const second = this.state.answerOrQuestion === 1 ? this.props.card.question : this.props.card.answer;
        const position = this.state.position;
        return (
            <div className="card w-25 deck-card">
                <div className="card-body">
                    <h5 className="card-title">{position === "notClicked" ? first : second}</h5>
                    {position === "notClicked" &&
                        <button className="btn btn-primary card-answer-button" onClick={this.handleKnowClick}>Знаю</button>}
                    {position != "clickedNotKnow"
                        && <button className="btn btn-primary card-answer-button" onClick={position === "clickedKnow" ?
                            () => { this.handleNotKnowClick(); this.handleNextCard("clickedNotKnow"); }
                            : () => this.handleNotKnowClick()}>Не знаю</button>}
                    {position != "notClicked" && <button className="btn btn-primary card-answer-button" onClick={() => this.handleNextCard("clickedKnow")}>Следующая</button>}
                </div>
            </div>
        );

    }

}
