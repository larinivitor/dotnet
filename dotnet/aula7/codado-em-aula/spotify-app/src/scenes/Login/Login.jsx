import React from 'react'

import RegisterForm from '../../components/RegisterForm/RegisterForm'
import LoginForm from '../../components/LoginForm/LoginForm'
import Alert from '../../components/generic/Alert/Alert'
import './Login.css'

const SELECTED_CONTENTS = {
    LOGIN: 'LOGIN',
    REGISTER: 'REGISTER'
}

export default class Login extends React.Component {

    constructor() {
        super()
        this.onClickLinkRegister = this.onClickLinkRegister.bind(this)
        this.onClickLinkLogin = this.onClickLinkLogin.bind(this)
        this.onRegisterSuccess = this.onRegisterSuccess.bind(this)
        this.state = {
            selectedContent: SELECTED_CONTENTS.LOGIN
        }
    }


    onClickLinkRegister() {
        this.setSelectedContent(SELECTED_CONTENTS.REGISTER)
    }

    onClickLinkLogin() {
        this.setSelectedContent(SELECTED_CONTENTS.LOGIN)
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    renderHomeContent() {
        return this.state.selectedContent === SELECTED_CONTENTS.LOGIN ? <LoginForm onLoginSuccess={this.props.onLoginSuccess} /> : <RegisterForm onRegisterSuccess={this.onRegisterSuccess} />
    }

    onRegisterSuccess() {
        this.setSelectedContent(SELECTED_CONTENTS.LOGIN)
        this.setState(({
            registered: true
        }))
    }

    renderRegisterSuccess() {
        return this.state.registered ? <Alert classAlert="primary" text="Cadastro realizado com sucesso, faça o login" /> : undefined
    }

    render() {
        return <div className="Login">
            <div className="Login--container">
                <div className="Login--header">
                    <h3>Por Favor, faça o  <span className="Login--link" onClick={this.onClickLinkLogin} >Login</span>, ou <span className="Login--link" onClick={this.onClickLinkRegister}>Cadastre-se</span></h3>
                </div>
                {this.renderRegisterSuccess()}
                <div className="App-content">
                    {this.renderHomeContent()}
                </div>
            </div>
        </div>
    }

}