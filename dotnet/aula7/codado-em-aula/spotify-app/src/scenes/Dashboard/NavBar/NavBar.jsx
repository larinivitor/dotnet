import React from 'react'

import Button from '../../../components/generic/Button/Button'

import LoginService from '../../../Services/LoginService'

import './NavBar.css'

export default class NavBar extends React.Component {

    constructor() {
        super()
        this.onClickLogoutButton = this.onClickLogoutButton.bind(this)
    }

    onClickLogoutButton() {
        LoginService.logout();
        this.props.onLogout();
    }

    render() {
        return <nav className="NavBar navbar navbar-dark bg-dark">
            <div className="NavBar--header">
                <span className="navbar-brand">Spotify</span>
            </div>
            <div className="NavBar--buttons">
                <Button onClick={this.onClickLogoutButton} isOutline={true} classButton="light" text="Logout" />
            </div>
        </nav>
    }

}