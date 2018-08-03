import CONFIG from '../config'
import axios from 'axios'

const LOGGED_USER = 'LOGGED_USER'

export default class LoginService {

	static setLoggedUser(token) {
		localStorage.setItem(LOGGED_USER, token)
	}

	static login(login, senha) {
		return axios.post(`${CONFIG.API_URL_BASE}/api/usuario/login`, {
			login,
			senha
		}).then((result) => {
			console.info(result);
			this.setLoggedUser(result.data.token)
			return result
		}).catch((err) => {
			console.error(err);
		})
	}

	static getLoggedUser() {
		return localStorage.getItem(LOGGED_USER)
	}

	static logout(login, senha) {
		localStorage.removeItem(LOGGED_USER);
	}
}