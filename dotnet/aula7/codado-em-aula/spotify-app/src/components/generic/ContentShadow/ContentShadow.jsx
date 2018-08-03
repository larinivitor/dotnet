import React from 'react'

import './ContentShadow.css'

export default class ContentShadow extends React.Component {

    render() {
        return <div className="ContentShadow">
            {this.props.children}
        </div>
    }
}

