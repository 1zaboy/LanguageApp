import React, { Component } from 'react';

export class EditorBox extends Component {
    constructor(props) {
        super(props);
        this.state = { userid: props.userid, wordw: "", table: [], value: '', textVal: this.props.textValue, dataToggle: "" };
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
        this.setState({ dataToggle: "" });
        this.loadData(event.target.innerText);        
    }
    loadData(text) {
        const userid = this.state.userid;
        //fetch(`/api/Workwords/GetLanguageWords?userid=${userid}&str=${text}`)            
        //    .then(res => this.setState({ dataToggle: res.text() }));


        fetch(`/api/Workwords/GetLanguageWords?userid=${userid}&str=${text}`)
            .then(response2 => response2.text())
            .then((jsonData) => {
                // jsonData is parsed json object received from url
                var rows = jsonData.split("|");
                var LRcolLan = [];
                var LRcolProc = [];
                rows.map((item) => {
                    LRcolLan.push(item.split(":")[0])
                    LRcolProc.push(item.split(":")[1])
                });

                var t = <table>
                    <tr>
                        <th>{LRcolLan[0]}</th>
                        <th>{LRcolProc[0]}</th>
                    </tr>
                    <tr>
                        <th>{LRcolLan[1]}</th>
                        <th>{LRcolProc[1]}</th>
                    </tr>
                    <tr>
                        <th>{LRcolLan[2]}</th>
                        <th>{LRcolProc[2]}</th>
                    </tr>
                    <tr>
                        <th>{LRcolLan[3]}</th>
                        <th>{LRcolProc[3]}</th>
                    </tr>
                    <tr>
                        <th>{LRcolLan[4]}</th>
                        <th>{LRcolProc[4]}</th>
                    </tr>                    
                </table >


                this.setState({ wordw: text, table: t })
                console.log(jsonData)
            })
            .catch((error) => {
                // handle your errors here
                this.setState({ wordw: "", table: "" })
                console.error(error)
            })

    }
    render() {

        var res = []
        //        var r = this.props.textValue.replace(/\B\p{L}+\B/gu, ":");
        if (this.state.value.trim() != "") {
            this.state.value && this.state.value.replace(/\p{L}+|[,]|[.]|[?]|[:]|[']|["]/gu, (md, link, text) => {
                res.push(md != ',' && md != '.' && md != '?' && md != ':' && md != '\'' && md != '\"' ?
                    <span onMouseOver={this.onMouseOverEvent} onInput={this.onChangeSpan}
                        data-toggle="tooltip" title={this.state.dataToggle} data-placement="top"> {md}
                        <span class="tooltiptext">Tooltip text</span>
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

                <h2>{this.state.wordw}</h2>
                <div>{this.state.table}</div>
            </div>
        );

    }
}

//<div contenteditable="true" className="user-text" class="text-editor-box" id="texteditor1" onInput={this.ChangeDivTextEditor}>
//    <p>text</p>
//    <p>{t}</p>
//</div>