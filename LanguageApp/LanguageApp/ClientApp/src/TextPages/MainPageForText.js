import React, { Component } from 'react';
import { TableData } from "../TextPages/TableData";
import { EditorBox } from "../TextPages/EditorBox";

export class TextPage extends Component {
    static displayName = TextPage.name;

    constructor(props) {
        super(props);
        this.state = { userid: props.userid, textValue: '', listChild: HTMLCollection, ItemsReactElement: [], value: '' };
    }

    handleChange(event) {
        this.setState({ value: event.target.value });
    }
    render() {
        return (
            <div class="container-fluid">
                <div class="row">
                    <div class="col-3">                        
                        <EditorBox userid={this.state.userid}/>                       
                        
                    </div>
                    <div class="col-9">
                        {React.createElement(TableData)}
                    </div>
                </div>
            </div>
        );
    }
}


