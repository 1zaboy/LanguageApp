import React, { Component } from 'react';
export class TableData extends Component {
    constructor(props) {
        super(props);
        
        this.LoadJson = this.LoadJson.bind(this);     
        this.state = { JsonStr: "", timer: '' };
        
    }
    componentDidMount() {
        this.intervalId = setInterval(this.LoadJson, 2000);
    }
    componentWillMount() {
        this.LoadJson();
    }

    LoadJson() {
        fetch(`/api/Datatable/GetJsonDataTopTan`)
            .then(response2 => response2.json())
            .then((jsonData) => {
                this.setState({ JsonStr: jsonData });
            })
    }

    render() {
        var arr = [];
        if (this.state.JsonStr !== '' && this.state.JsonStr != null) {
            this.state.JsonStr.map((item) => {
                arr.push(<tr>
                    <td>{item.NameUser}</td>
                    <td>{item.CountRequests}</td>
                    <td>{item.LastLogin}</td>
                    <td>{item.AvgTimeRequests}</td>
                </tr>);
            });
        }
        
        return (
            <table class="table">
                <tr>
                    <th>User</th>
                    <th>Count requests</th>
                    <th>DateTime last login</th>
                    <th>Average time between requests</th>
                </tr>
                {arr}
                
            </table>
            );
    }
}
