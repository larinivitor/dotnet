import React from 'react'
export default class Button extends React.Component {

    getClassName() {
        let classButton = 'btn'
        if (this.props.isOutline) {
            classButton += '-outline'
        }
        classButton += `-${this.props.classButton}`
        return `btn ${classButton}`
    }
    render() {
        return <button className={this.getClassName()}
            onClick={this.props.onClick}
            type={this.props.type}>{this.props.text}</button>
    }

}