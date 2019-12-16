import React, { Component } from 'react';
import { TextPage } from '../TextPages/MainPageForText';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = { userid: "", currentCount: 0, ValueName: '', ValuePassword: '', ValuePassword2: '', ResponseText: '', dopPoly: false, isLog: false };
        this.incrementCounter = this.incrementCounter.bind(this);

        this.NameChange = this.NameChange.bind(this);
        this.PasswordChange = this.PasswordChange.bind(this);
        this.PasswordChange2 = this.PasswordChange2.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.loadData = this.loadData.bind(this);
        this.loadData2 = this.loadData2.bind(this);
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

    PasswordChange2(event) {
        this.setState({ ValuePassword2: event.target.value });
    }

    handleSubmit(event) {
        if (!this.state.dopPoly) {
            this.loadData();
        }
        else {
            this.loadData2();
        }
        
        event.preventDefault();
    }

    render() {
        var t = "";
        if (this.state.dopPoly) {
            t = <div class="form-group"> <label for="exampleInputPassword1">
                Password:</label>
                <input type="password" id="exampleInputPassword2" class="form-control" value={this.state.ValuePassword2} onChange={this.PasswordChange2} />
            </div>
        }
        var res = "";
        if (!this.state.isLog) {
            res = <div>
                <div class="row">
                    <div class="col-3">
                <h1>Login</h1>
                <form  onSubmit={this.handleSubmit}>
                    <div class="form-group">
                        <label for="exampleInputEmail1">
                            Name:</label>
                        <input type="text" id="exampleInputEmail1" class="form-control" value={this.state.ValueName} onChange={this.NameChange} />
                        
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">
                            Password:</label>
                        <input type="password" class="form-control" value={this.state.ValuePassword} id="exampleInputPassword1" onChange={this.PasswordChange} />
                        
                    </div>
                    <div>{t}</div>
                            <input type="submit" class="btn btn-primary" value="Send" />
                        </form>
                    </div>
                    <div class="col-9"></div>
                    </div>
            </div>
        }
        else {
            res = <TextPage userid={this.state.userid}/>
        }

        return (    
        <div>
                {res}
                </div>
        );
    }




    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "/api/Login/IsLoginCompleted?name=" + this.state.ValueName + "&password=" + this.state.ValuePassword, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            //this.setState({ ResponseText: data });
            if (data === -1) {
                alert("Error enter name or/and password");
                this.setState({ dopPoly: true });
            } else {
                //browserHistory.push("/TextPages");
                this.setState({ isLog: true, dopPoly: false, userid: data });                
            }
        }.bind(this);
        xhr.send();
    }

    loadData2() {
        var xhr = new XMLHttpRequest();
        if (this.state.ValuePassword === this.state.ValuePassword2) {
            xhr.open("get", "/api/Login/AddUser?name=" + this.state.ValueName + "&password=" + this.state.ValuePassword, true);
            xhr.onload = function () {
                var data = JSON.parse(xhr.responseText);                
                if (data === -1) {
                    alert("Error");                    
                } else {
                    this.setState({ isLog: true, dopPoly: false, userid: data });                     
                }
            }.bind(this);
            xhr.send();
        }
        else {
            alert("Error pass1 != pass2");
        }
    }
}
