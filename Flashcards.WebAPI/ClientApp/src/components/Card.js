import React, { Component } from 'react';

export class Card extends Component {
    static displayName = Card.name;

    constructor(props) {
        super(props);
        this.state = {"position": "notClicked"};
     }

     render()
     {
         if (this.props.card == undefined)
            return (<b>Пожалуйста, подождите...</b>)
         return (
             <li>{this.props.card.answer}</li>
         );
     }


}
