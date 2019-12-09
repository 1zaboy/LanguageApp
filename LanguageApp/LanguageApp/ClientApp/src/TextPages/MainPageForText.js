import React, { Component } from 'react';
import { render } from "react-dom";
import { Word } from "../TextPages/Word";
import { Line } from "../TextPages/Line";
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
                    <div class="col">
                        <div contenteditable="true" class="text-editor-box" onInput={this.ChangeDivTextEditor} onPaste={this.onPaste}>
                            <div>фыв уцке цуке</div>
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




    ChangeDivTextEditor(event) {
         var listChildren = event.target.children;
        for (var i = 0; i < listChildren.length; i++) {
            var listStrSplit = listChildren[i].innerText.split(' ');            

            
            var arrayDom = [];

            //listChildren[i].innerHTML = "&nbsp;";
            //for (var ii = 0; ii < listStrSplit.length; ii++) {
            //    arrayDom.push(React.createElement(Word, { textValue: listStrSplit[ii] }));   
            //}               
            


            render(React.createElement(Line, { textValue: listChildren[i].innerText }), listChildren[i]);         
        }        
    }
}


