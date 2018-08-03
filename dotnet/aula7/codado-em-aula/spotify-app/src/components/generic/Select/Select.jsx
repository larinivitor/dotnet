import React from 'react'
export default class Select extends React.Component {

    renderOptions() {
        const options = this.props.options.map((option, key) => {
            return <option key={key} value={option.value}>{option.text}</option>
        })
        if (this.props.showEmptyOption) {
            const emptyOption = <option key={-1} ></option>
            return [emptyOption, ...options]
        }
        return options

    }

    render() {
        return <div className="form-group">
            <label >{this.props.label}</label>
            <select name={this.props.name} value={this.props.value} onChange={this.props.handleChange} className="form-control" >
                {this.renderOptions()}
            </select>
        </div>
    }
}