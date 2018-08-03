import React from 'react'

import Button from '../Button/Button'

import './Modal.css'



export default class Modal extends React.Component {

    getShowClass() {
        const showClass = this.props.show ? 'Modal--show' : ''
        return `Modal ${showClass}`

    }

    render() {
        return <div className={this.getShowClass()}>
            <div className="modal-dialog" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">{this.props.title}</h5>
                    </div>
                    <div className="modal-body">
                        {this.props.text}
                    </div>
                    <div className="modal-footer">
                        <Button classButton="secondary" onClick={this.props.onClose} text="Fechar" />

                        {this.props.classButtonAction ? <Button classButton={this.props.classButtonAction}
                            onClick={this.props.onClickButtonAction}
                            text={this.props.textButtonAction} /> : undefined}
                    </div>
                </div>
            </div>
        </div>
    }

}