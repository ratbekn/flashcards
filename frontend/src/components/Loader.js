import React, { Component } from 'react';
import "./Card.css"

export class Loader extends Component {
    static displayName = Loader.name;

    constructor(props) {
        super(props);

    }

    render() {
        return (
            (<div className="d-flex justify-content-center">
                <div className="spinner-border" role="status">
                    <span className="sr-only">Загружаем...</span>
                </div>
            </div>)
        );
    }
}