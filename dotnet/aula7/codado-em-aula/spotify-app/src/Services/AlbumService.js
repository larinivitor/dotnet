import CONFIG from '../config'
import axios from 'axios'

import LoginService from './LoginService'

export default class AlbumService {

	static getAlbums() {
		return axios.get(`${CONFIG.API_URL_BASE}/api/album`,
			{
				headers: {
					Authorization: `Bearer ${LoginService.getLoggedUser()}`,
					'Content-Type': 'application/json',
				}
			}
		).then((albums) => {
			console.info(albums);
			return albums.data;
		}).catch((err) => {
			console.error(err);
			return [];
		})
	}
}