import React, { Component } from 'react';
import { render } from "react-dom";
import { Line } from "../TextPages/Line";
import { TableData } from "../TextPages/TableData";
import { EditorBox } from "../TextPages/EditorBox";
import { $ } from "jquery";

export class TextPage extends Component {
    static displayName = TextPage.name;

    constructor(props) {
        super(props);
        this.state = { userid: props.userid, textValue: '', listChild: HTMLCollection, ItemsReactElement: [], value: '' };
        this.ChangeDivTextEditor = this.ChangeDivTextEditor.bind(this);        
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




    ChangeDivTextEditor(event) {

        var listChildren = event.target.children;
        if (listChildren.length != this.state.ItemsReactElement.length) {
            var countStepForCildrenDiv = (listChildren.length - this.state.ItemsReactElement.length);
            if (countStepForCildrenDiv > 0) {
                for (var i = 0; i < Math.abs(countStepForCildrenDiv); i++) {
                    this.state.ItemsReactElement.push(React.createElement(Line, { textValue: listChildren[i].innerText }));
                }
            }
            //} else {
            //    for (var i = 0; i < Math.abs(countStepForCildrenDiv); i++) {
            //        this.state.ItemsReactElement.splice(listChildren.length, this.state.ItemsReactElement.length - listChildren.length);
            //    }
            //}            
        }       

        
            

        for (var i = 0; i < this.state.ItemsReactElement.length; i++) {
            //this.state.ItemsReactElement[i].LoadInfoLanguage();
            //this.state.ItemsReactElement[i].props.textValue = listChildren[i].innerText;
            //this.state.ItemsReactElement[i].setStateObject(listChildren[i].innerText);
            //this.state.ItemsReactElement[i].setState({ textVal: listChildren[i].innerText });
            //render(this.state.ItemsReactElement[i]);
            render(this.state.ItemsReactElement[i], listChildren[i]);
        }


        for (var i = 0; i < listChildren.length; i++) {

        //    //listChildren[i].innerHTML = "&nbsp;";
        //    //for (var ii = 0; ii < listStrSplit.length; ii++) {
        //    //    arrayDom.push(React.createElement(Word, { textValue: listStrSplit[ii] }));   
        //    //}                           

            this.state.ItemsReactElement.push(React.createElement(Line, { textValue: listChildren[i].innerText }));
            render(this.state.ItemsReactElement[this.state.ItemsReactElement.length - 1], listChildren[i]);
        }
    }
}


