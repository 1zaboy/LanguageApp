import React, { Component } from 'react';
import { render } from "react-dom";
import { Word } from "../TextPages/Word";
import { Line } from "../TextPages/Line";
import { TableData } from "../TextPages/TableData";
export class TextPage extends Component {
    static displayName = TextPage.name;

    constructor(props) {
        super(props);
        this.state = { textValue: '', listChild: HTMLCollection };
        this.ChangeDivTextEditor = this.ChangeDivTextEditor.bind(this);
    }

    render() {
        return ( 
            <div class="container-fluid">
                <div class="row">
                    <div class="col-3">
                        <div contenteditable="true" class="text-editor-box" onInput={this.ChangeDivTextEditor}>
                            <div>фыв,уцке,цуке</div>
                        </div>                       
                    </div>
                    <div class="col-9">
                        {React.createElement(TableData)}
                    </div>
                </div>
            </div>
        );
    }




    ChangeDivTextEditor(event) {
         var listChildren = event.target.children;
        for (var i = 0; i < listChildren.length; i++) {
            var listStrSplit = listChildren[i].innerText.split(' ');            

            
            var arrayDom = [];

            //listChildren[i].innerHTML = "&nbsp;";
            //for (var ii = 0; ii < listStrSplit.length; ii++) {
            //    arrayDom.push(React.createElement(Word, { textValue: listStrSplit[ii] }));   
            //}               
            

            var t = React.createElement(Line, { textValue: listChildren[i].innerText });
            render(t, listChildren[i]);         
        }        
    }
}


