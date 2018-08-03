import React from 'react'

export default class Jumbotron extends React.Component {
    render() {
        return <div className="jumbotron">
            <h1 className="display-4">{this.props.title}</h1>
            <p className="lead">{this.props.description}</p>
        </div>
    }
}

