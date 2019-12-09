import React, { Component } from 'react';
const asd = {
"movies": [
    {
        "abridged_cast": [
            {
                "characters": [
                    "Dominic Toretto"
                ],
                "id": "162652472",
                "name": "Vin Diesel"
            },
            {
                "characters": [
                    "Brian O'Conner"
                ],
                "id": "162654234",
                "name": "Paul Walker"
            },
            {
                "characters": [
                    "Louie Tran"
                ],
                "id": "162684066",
                "name": "Tony Jaa"
            },
            {
                "characters": [
                    "Deckard Shaw"
                ],
                "id": "162653720",
                "name": "Jason Statham"
            },
            {
                "characters": [
                    "Luke Hobbs"
                ],
                "id": "770893686",
                "name": "Dwayne \"The Rock\" Johnson"
            }
        ],
        "alternate_ids": {
            "imdb": "2820852"
        },
        "critics_consensus": "",
        "id": "771354922",
        "links": {
            "alternate": "http://www.rottentomatoes.com/m/furious_7/",
            "cast": "http://api.rottentomatoes.com/api/public/v1.0/movies/771354922/cast.json",
            "reviews": "http://api.rottentomatoes.com/api/public/v1.0/movies/771354922/reviews.json",
            "self": "http://api.rottentomatoes.com/api/public/v1.0/movies/771354922.json",
            "similar": "http://api.rottentomatoes.com/api/public/v1.0/movies/771354922/similar.json"
        },
        "mpaa_rating": "PG-13",
        "posters": {
            "detailed": "http://resizing.flixster.com/pVDoql2vCTzNNu0t6z0EUlE5G_c=/51x81/dkpu1ddg7pbsk.cloudfront.net/movie/11/18/14/11181482_ori.jpg",
            "original": "http://resizing.flixster.com/pVDoql2vCTzNNu0t6z0EUlE5G_c=/51x81/dkpu1ddg7pbsk.cloudfront.net/movie/11/18/14/11181482_ori.jpg",
            "profile": "http://resizing.flixster.com/pVDoql2vCTzNNu0t6z0EUlE5G_c=/51x81/dkpu1ddg7pbsk.cloudfront.net/movie/11/18/14/11181482_ori.jpg",
            "thumbnail": "http://resizing.flixster.com/pVDoql2vCTzNNu0t6z0EUlE5G_c=/51x81/dkpu1ddg7pbsk.cloudfront.net/movie/11/18/14/11181482_ori.jpg"
        },
        "ratings": {
            "audience_rating": "Upright",
            "audience_score": 88,
            "critics_rating": "Certified Fresh",
            "critics_score": 82
        },
        "release_dates": {
            "theater": "2015-04-03"
        },
        "runtime": 140,
        "synopsis": "Continuing the global exploits in the unstoppable franchise built on speed, Vin Diesel, Paul Walker and Dwayne Johnson lead the returning cast of Fast & Furious 7. James Wan directs this chapter of the hugely successful series that also welcomes back favorites Michelle Rodriguez, Jordana Brewster, Tyrese Gibson, Chris \"Ludacris\" Bridges, Elsa Pataky and Lucas Black. They are joined by international action stars new to the franchise including Jason Statham, Djimon Hounsou, Tony Jaa, Ronda Rousey and Kurt Russell.",
        "title": "Furious 7",
        "year": 2015
    }
]
}
export class Line extends Component {
    constructor(props) {
        super(props);
        this.state = { textVal: this.props.textValue, dataToggle: "", lan1: 0,lan2: 0, len3: 0, lan4: 0, lan5: 0 };
        this.onMouseOverEvent = this.onMouseOverEvent.bind(this);
        this.onChangeSpan = this.onChangeSpan.bind(this);
        this.LoadInfoLanguage = this.LoadInfoLanguage.bind(this);
    }

    onMouseOverEvent(event) {
        var textf = "";//<h1>"+event.target.innerText+"</h1>";
        this.setState({ dataToggle: textf });    
        this.LoadInfoLanguage();
    }

    LoadInfoLanguage() {
        var myObject = JSON.parse(asd);
    }

    onChangeSpan(event) {
        this.setState({ textVal: this.getText(event.target) });
    }

    componentWillReceiveProps(nextProps) {
        this.setState({ textVal: this.props.textValue });        
    }

    render() {
        var a = this.props.textValue.split(",");
        
        console.error(this.state.textVal);
        return (
            a.map((text) =>                
                <span onMouseOver={this.onMouseOverEvent} onInput={this.onChangeSpan}
                    data-toggle="tooltip" title={this.state.dataToggle} data-placement="top">{text}   
                </span>                
            )
        );
    }



}

/////////////////////////https://ru.stackoverflow.com/questions/767440/%d0%93%d0%b4%d0%b5-%d0%b2%d0%b7%d1%8f%d1%82%d1%8c-%d0%b1%d0%b0%d0%b7%d1%83-%d1%81%d0%bb%d0%be%d0%b2-%d0%b4%d0%bb%d1%8f-%d0%a0%d1%83%d1%81%d1%81%d0%ba%d0%be-%d0%90%d0%bd%d0%b3%d0%bb%d0%b8%d0%b9%d1%81%d0%ba%d0%be%d0%b3%d0%be-%d1%81%d0%bb%d0%be%d0%b2%d0%b0%d1%80%d1%8f