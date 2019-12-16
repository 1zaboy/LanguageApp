import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './Accaunt/Login';
import { TextPage } from './TextPages/MainPageForText';
import PrivateRoute from './Accaunt/PrivateRoute';
import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>                
                <Route path='/Login' component={Login} />                
                <PrivateRoute path='/' component={TextPage} />
                <PrivateRoute path='/text' component={TextPage} />
            </Layout>
        );
    }
}
