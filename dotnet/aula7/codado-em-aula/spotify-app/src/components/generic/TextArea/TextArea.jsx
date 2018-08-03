import React from 'react'
export default class TextArea extends React.Component {

    render() {
        return <div className="form-group">
            <label htmlFor="exampleInputEmail1">{this.props.label}</label>
            <textarea
                onChange={this.props.handleChange}
                value={this.props.value}
                type={this.props.type}
                className="form-control"
                name={this.props.name}
                placeholder={this.props.placeholder} />
        </div>
    }

}