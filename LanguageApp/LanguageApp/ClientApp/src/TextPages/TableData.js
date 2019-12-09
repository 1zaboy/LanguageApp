import React, { Component } from 'react';
export class TableData extends Component {
    constructor(props) {
        super(props);
        this.state = { JsonStr: "" };
        this.LoadJson = this.LoadJson.bind(this);
    }

    LoadJson() {

    }

    render() {
        return (
            <table>
                <tr>
                    <th>User</th>
                    <th>Count requests</th>
                    <th>DateTime last login</th>
                    <th>Average time between requests</th>
                </tr>
                <tr>
                    <td>Jill</td>
                    <td>Smith</td>
                    <td>50</td>
                    <td>50</td>
                </tr>                
            </table>
            );
    }
}
