import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { CreateDeck } from './components/CreateDeck';
import { EditDeck } from './components/EditDeck';
import { Deck } from './components/Deck';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/add' component={CreateDeck} />
        <Route path='/deck/:id' component={Deck} />
        <Route path='/edit/:id' component={EditDeck} />
      </Layout>
    );
  }
}
