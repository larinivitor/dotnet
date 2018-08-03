import CONFIG from '../config'
import axios from 'axios'

export default class RegisterService {

    static register(login, senha, primeiroNome, ultimoNome, idade, email, telefone) {
        return axios.post(`${CONFIG.API_URL_BASE}/api/usuarios`, {
            login,
            senha,
            primeiroNome,
            ultimoNome,
            idade,
            email,
            telefone
        })
    }
}