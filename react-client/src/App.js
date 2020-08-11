import React, { Component } from 'react'
import { Route } from 'react-router'
import { Layout } from './components/Layout'
import { Home } from './components/Home'
import { AddMember } from './components/AddMember'
import { EditMember } from './components/EditMember'

import './custom.css'

export default class App extends Component {
  static displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/add-member" component={AddMember} />
        <Route path="/edit-member" component={EditMember} />
      </Layout>
    )
  }
}
