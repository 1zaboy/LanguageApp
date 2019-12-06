import React, { Component } from 'react';
export class TextPage extends Component {
    static displayName = TextPage.name;

    constructor(props) {
        super(props);
        this.state = { textValue: '' };

    }

    render() {
        return (
            <div class="container-fluid">
                <div class="row">
                    <div class="col">
                        <div contenteditable="true" spellcheck="false" class="text-editor-box">
                            <p>selected text</p>
                        </div>
                    </div>
                    <div class="col">
                        <table>
                            <tr>
                                <th>Firstname</th>
                                <th>Lastname</th>
                                <th>Age</th>
                            </tr>
                            <tr>
                                <td>Jill</td>
                                <td>Smith</td>
                                <td>50</td>
                            </tr>
                            <tr>
                                <td>Eve</td>
                                <td>Jackson</td>
                                <td>94</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        );
    }
}