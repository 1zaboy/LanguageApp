import React, { Component } from 'react';
import ReactHtmlParser, { processNodes, convertNodeToElement, htmlparser2 } from 'react-html-parser';

export class EditorBox extends Component {
    constructor(props) {
        super(props);
        this.state = { value: '', textVal: this.props.textValue, dataToggle: "", lan1: 0, lan2: 0, len3: 0, lan4: 0, lan5: 0 };
        this.onChangeSpan = this.onChangeSpan.bind(this);
        this.ChangeDivTextEditor = this.ChangeDivTextEditor.bind(this);        
        this.handleChange = this.handleChange.bind(this);
        this.onMouseOverEvent = this.onMouseOverEvent.bind(this);
        this.loadData = this.loadData.bind(this);
    }

    onChangeSpan(event) {
        this.setState({ textVal: this.getText(event.target) });
    }
    ChangeDivTextEditor(event) {

    }
    handleChange(event) {
        this.setState({ value: event.target.value });
    }
    onMouseOverEvent(event) {
        this.loadData(event.target.innerText);
    }    
    loadData(text) {
        const userid = 1;        
        //fetch(`/api/Workwords/GetLanguageWords?userid=${userid}&str=${text}`)            
        //    .then(res => this.setState({ dataToggle: res.text() }));


        fetch(`/api/Workwords/GetLanguageWords?userid=${userid}&str=${text}`)
            .then(response2 => response2.text())
                .then((jsonData) => {
                    // jsonData is parsed json object received from url
                    this.setState({ dataToggle: jsonData})
                    console.log(jsonData)
                })
                .catch((error) => {
                    // handle your errors here
                    console.error(error)
                })


        //var xhr = new XMLHttpRequest();
        //xhr.open("get", this.props.apiUrl, true);
        //xhr.onload = function () {
        //    var data = JSON.parse(xhr.responseText);
        //    this.setState({ phones: data });
        //}.bind(this);
        //xhr.send();
    }
    render() {

        var res = []
        //        var r = this.props.textValue.replace(/\B\p{L}+\B/gu, ":");
        if (this.state.value.trim() != "") {
            this.state.value && this.state.value.replace(/\p{L}+|[,]|[.]|[?]|[:]|[']|["]/gu, (md, link, text) => {
                res.push(md != ',' && md != '.' && md != '?' && md != ':' && md != '\'' && md != '\"' ?
                    <span onMouseOver={this.onMouseOverEvent} onInput={this.onChangeSpan}
                        data-toggle="tooltip" title={this.state.dataToggle} data-placement="top"> {md}
                    </span> : md)
            })
        } else {
            res.push(<br />);
        }

        return (
            //<div className="user-text" ></div>
            <div>
                <div>
                    {res}
                </div>
                

                <input type="text" name="browser" value={this.state.value} onChange={this.handleChange} />

            </div>
        );

    }
}

//<div contenteditable="true" className="user-text" class="text-editor-box" id="texteditor1" onInput={this.ChangeDivTextEditor}>
//    <p>text</p>
//    <p>{t}</p>
//</div>