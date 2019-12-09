import React, { Component } from 'react';
export class Line extends Component {
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

    componentWillReceiveProps(nextProps) {
        this.setState({ textVal: this.props.textValue });
        //if (nextProps.textValue !== this.props.textValue) {
            
        //}
    }

    render() {        
        var t = this.state.textVal.split(" ");
        console.error(this.state.textVal);      
        return (

            t.map((text) =>
                <span onMouseOver={this.onMouseOverEvent} onInput={this.onChangeSpan} data-toggle="tooltip" title={this.state.dataToggle} data-placement="top">{text}&nbsp;</span>
                
                //<span>{text}</span>
            )

        );
    }
}