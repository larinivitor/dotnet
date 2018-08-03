import React from 'react'
import jwt_decode from 'jwt-decode';

import Albums from './Albums/Albums'
import NavBar from './NavBar/NavBar'

import './Dashboard.css'

import LoginService from '../../Services/LoginService'


export default class Dashboard extends React.Component {

    constructor() {
        super()
        this.getTokenInfo = this.getTokenInfo.bind(this)
    }

    getTokenInfo() {
        let token = LoginService.getLoggedUser();
        let decoded = jwt_decode(token);
        console.log(decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"])
        return JSON.stringify(decoded)
    }

    render() {
        return <div>
            <div className="Dashboard--header">
                <NavBar onLogout={this.props.onLogout} />
            </div>

            <div className="Dashboard--content">
                <Albums />
            </div>

            <div>
                Token info: {this.getTokenInfo()}
            </div>
        </div>
    }
}