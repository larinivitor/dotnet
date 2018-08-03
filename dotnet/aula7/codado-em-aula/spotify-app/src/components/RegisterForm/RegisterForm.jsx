import React from 'react'

import Input from '../generic/Input/Input'
import Button from '../generic/Button/Button'
import Alert from '../generic/Alert/Alert'

import RegisterService from '../../Services/RegisterService'

import './RegisterForm.css'

export default class RegisterForm extends React.Component {

    constructor() {
        super()
        this.state = this.getInitialState()

        this.handleChange = this.handleChange.bind(this)
        this.onRegisterButtonClick = this.onRegisterButtonClick.bind(this)
    }

    getInitialState() {
        return {
            login: '',
            senha: '',
            primeiroNome: '',
            ultimoNome: '',
            idade: '',
            email: '',
            telefone: ''
        }
    }

    handleChange(event) {
        const target = event.target
        const value = target.value
        const name = target.name
        this.setState({
            [name]: value
        })
    }

    onRegisterButtonClick() {
        RegisterService.register(
            this.state.login,
            this.state.senha,
            this.state.primeiroNome,
            this.state.ultimoNome,
            this.state.idade,
            this.state.email,
            this.state.telefone).then((result) => {
                debugger;
                this.props.onRegisterSuccess()
            }).catch(resp => {
                debugger;
                this.setState({
                    error: resp.response.data.error
                })
            })
    }

    renderError() {
        return this.state.error ? <Alert classAlert="danger" text={this.state.error} /> : undefined
    }

    render() {
        return (
            <div>
                {this.renderError()}
                <Input placeholder="Login" name="login" type="text" handleChange={this.handleChange} label="Login" />
                <Input placeholder="Senha" name="senha" type="password" handleChange={this.handleChange} label="Senha" />
                <Input placeholder="Primeiro Nome" name="primeiroNome" type="text" handleChange={this.handleChange} label="Primeiro Nome" />
                <Input placeholder="Ultimo Nome" name="ultimoNome" type="text" handleChange={this.handleChange} label="Ultimo Nome" />
                <Input placeholder="Idade" name="idade" type="text" handleChange={this.handleChange} label="Idade" />
                <Input placeholder="Email" name="email" type="text" handleChange={this.handleChange} label="Email" />
                <Input placeholder="Telefone" name="telefone" type="text" handleChange={this.handleChange} label="Telefone" />
                <div className="pull-right">
                    <Button isOutline={true} classButton="primary" type="button" onClick={this.onRegisterButtonClick} text="Registrar" />
                </div>
            </div>
        )
    }
}