import React, { Component } from 'react';

export class Line extends Component {
    constructor(props) {
        super(props);
        this.state = { textVal: this.props.textValue, dataToggle: "", lan1: 0, lan2: 0, len3: 0, lan4: 0, lan5: 0 };
        this.onMouseOverEvent = this.onMouseOverEvent.bind(this);
        this.onChangeSpan = this.onChangeSpan.bind(this);
        this.LoadInfoLanguage = this.LoadInfoLanguage.bind(this);
    }

    onMouseOverEvent(event) {
        var textf = event.target.innerText;//<h1>"+event.target.innerText+"</h1>";
        this.setState({ dataToggle: textf });
        //this.LoadInfoLanguage();
    }

    LoadInfoLanguage() {
        alert();
    }

    onChangeSpan(event) {
        this.setState({ textVal: this.getText(event.target) });
    }

    componentWillReceiveProps(nextProps) {
        this.setState({ textVal: this.props.textValue });
    }


    render() {

        var a = this.props.textValue;//.split(" ");        
        console.error(this.state.textVal);



        var res = []                
//        var r = this.props.textValue.replace(/\B\p{L}+\B/gu, ":");
        if (this.props.textValue.trim() != "") {
            this.props.textValue && this.props.textValue.replace(/\p{L}+|[,]|[.]|[?]|[:]|[']|["]/gu, (md, link, text) => {
                res.push(md != ',' && md != '.' && md != '?' && md != ':' && md != '\'' && md != '\"' ?
                    <span onMouseOver={this.onMouseOverEvent} onInput={this.onChangeSpan}
                        data-toggle="tooltip" title={this.state.dataToggle} data-placement="top"> {md}
                    </span> : md)
            })
        } else {
            res.push(<br/>);
        }
        
        return (
        <div className="user-text">{res}</div>
        
        );
    }
}

/////////////////////////https://ru.stackoverflow.com/questions/767440/%d0%93%d0%b4%d0%b5-%d0%b2%d0%b7%d1%8f%d1%82%d1%8c-%d0%b1%d0%b0%d0%b7%d1%83-%d1%81%d0%bb%d0%be%d0%b2-%d0%b4%d0%bb%d1%8f-%d0%a0%d1%83%d1%81%d1%81%d0%ba%d0%be-%d0%90%d0%bd%d0%b3%d0%bb%d0%b8%d0%b9%d1%81%d0%ba%d0%be%d0%b3%d0%be-%d1%81%d0%bb%d0%be%d0%b2%d0%b0%d1%80%d1%8f