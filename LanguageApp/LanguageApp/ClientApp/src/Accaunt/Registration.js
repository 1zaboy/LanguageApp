﻿import React, { Component } from 'react';

export class Registration extends Component {
    static displayName = Registration.name;

    constructor(props) {
        super(props);
        this.state = { currentCount: 0, ValueName: '', ValuePassword: '', ResponseText: '' };
        this.incrementCounter = this.incrementCounter.bind(this);

        this.NameChange = this.NameChange.bind(this);
        this.PasswordChange = this.PasswordChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.loadData = this.loadData.bind(this);
    }
    incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }

    NameChange(event) {
        this.setState({ ValueName: event.target.value });
    }

    PasswordChange(event) {
        this.setState({ ValuePassword: event.target.value });
    }

    handleSubmit(event) {
        this.loadData();
        event.preventDefault();
    }

    render() {
        return (
            <div>
                <h1>Registration</h1>
                <form onSubmit={this.handleSubmit}>
                    <div>
                        <label>
                            Name:
                    <input type="text" value={this.state.ValueName} onChange={this.NameChange} />
                        </label>
                    </div>
                    <div>
                        <label>
                            Password:
                    <input type="text" value={this.state.ValuePassword} onChange={this.PasswordChange} />
                        </label>
                    </div>
                    <input type="submit" value="Send" />
                </form>
            </div>
        );
    }




    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "/api/Login/AddUser?" + 'name=' + this.state.ValueName + '&password=' + this.state.ValuePassword, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ ResponseText: data });
        }.bind(this);
        xhr.send();
    }
}
