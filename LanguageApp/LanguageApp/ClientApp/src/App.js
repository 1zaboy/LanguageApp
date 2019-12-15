import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Login } from './Accaunt/Login';
import { Registration } from './Accaunt/Registration';
import { TextPage } from './TextPages/MainPageForText';
import AuthorizeRoute from './api-authorization/AuthorizeRoute';
import PrivateRoute from './Accaunt/PrivateRoute';
import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>                
                <AuthorizeRoute path='/Login' component={Login} />
                <Route path='/Registration' component={Registration} />
                <Route path='/fetch-data' component={FetchData} />
                <PrivateRoute path='/TextPages' component={TextPage} />
            </Layout>
        );
    }
}
