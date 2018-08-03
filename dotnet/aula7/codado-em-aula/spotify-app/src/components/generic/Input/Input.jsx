import React from 'react'
export default class Input extends React.Component {

    render() {
        return <div className="form-group">
            <input
                onChange={this.props.handleChange}
                value={this.props.value}
                type={this.props.type}
                className="form-control"
                name={this.props.name}
                placeholder={this.props.placeholder} />
        </div>
    }

}