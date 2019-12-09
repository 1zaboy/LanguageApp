import React, { Component } from 'react';
export class Word extends Component {
    constructor(props) {
        super(props);
        this.state = { textVal: this.props.textValue, dataToggle: "" };
        this.onMouseOverEvent = this.onMouseOverEvent.bind(this);
        this.onChangeSpan = this.onChangeSpan.bind(this);
    }

    onMouseOverEvent(event) {
        var textf = this.state.textVal;
        this.setState({ dataToggle: textf });
        //alert("asd");
    }

    LoadInfoLanguage() {

    }

    onChangeSpan(event) {
        this.setState({ textVal: this.getText(event.target) });
    }

    render() {
        return (
            <span onMouseOver={this.onMouseOverEvent} onInput={this.onChangeSpan} data-toggle="tooltip" title={this.state.dataToggle} data-placement="top">{this.state.textVal}</span>
            );
    }
}