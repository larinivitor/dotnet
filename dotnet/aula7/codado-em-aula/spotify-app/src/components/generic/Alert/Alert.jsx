import React from 'react'
export default class Alert extends React.Component {

    getClassName() {
        return `alert alert-${this.props.classAlert}`
    }
    render() {
        return <div className={this.getClassName()} role="alert">
            {this.props.text}
        </div>
    }

}