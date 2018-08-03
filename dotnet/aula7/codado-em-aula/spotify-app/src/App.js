import React, { Component } from 'react';
import axios from 'axios'


import Loading from './components/generic/Loading/Loading'

import Dashboard from './scenes/Dashboard/Dashboard'
import Login from './scenes/Login/Login'

import LoginService from './Services/LoginService'

import './App.css'


const CONTENTS = {
	LOGIN: 'LOGIN',
	DASHBOARD: 'DASHBOARD'
}


class App extends Component {

	constructor() {
		super()
		this.configureRequestInterceptor()
		this.onLoginSuccess = this.onLoginSuccess.bind(this)
		this.onLogout = this.onLogout.bind(this)
		this.state = {}
	}

	componentDidMount() {
		this.resolveSelectedContent()
	}

	resolveSelectedContent() {
		if (LoginService.getLoggedUser()) {
			this.setSelectedContent(CONTENTS.DASHBOARD)
			return
		}
		this.setSelectedContent(CONTENTS.LOGIN)
	}

	setSelectedContent(content) {
		this.setState({
			selectedContent: content
		})
	}

	configureRequestInterceptor() {
		const self = this;
		axios.interceptors.request.use((config) => {
			self.showLoader(true)
			return config;
		});

		axios.interceptors.response.use((response) => {
			self.showLoader(false)
			return response;
		}, (error) => {
			if (error.response.status === 401) {
				self.setSelectedContent(CONTENTS.LOGIN)
			}
			self.showLoader(false)
			return Promise.reject(error)
		})
	}

	showLoader(showLoader) {
		this.setState({
			showLoader
		})
	}

	onLoginSuccess() {
		this.setSelectedContent(CONTENTS.DASHBOARD)
	}

	renderContent() {
		if (this.state.selectedContent === CONTENTS.DASHBOARD) {
			return <Dashboard onLogout={this.onLogout} />
		}
		if (this.state.selectedContent === CONTENTS.LOGIN) {
			return <Login onLoginSuccess={this.onLoginSuccess} />
		}
	}

	onLogout() {
		this.setSelectedContent(CONTENTS.LOGIN)
	}

	render() {
		return (
			<div className="App">
				<Loading showLoader={this.state.showLoader} />
				{this.renderContent()}
			</div>
		)
	}
}

export default App;
