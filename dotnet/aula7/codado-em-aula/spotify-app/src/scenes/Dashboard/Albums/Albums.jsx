import React from 'react'

import AlbumCard from './AlbumCard/AlbumCard'
import Modal from '../../../components/generic/Modal/Modal'
import Jumbotron from '../../../components/generic/Jumbotron/Jumbotron'

import AlbumService from '../../../Services/AlbumService'

import './Albums.css'

export default class Albums extends React.Component {

	constructor() {
		super()
		this.state = {
			albums: []
		}
	}

	componentDidMount() {
		this.loadAlbums()
	}

	loadAlbums() {
		AlbumService.getAlbums().then((albumList) => {
			this.setState({
				albums: albumList
			})
		}).catch((err) => {
			console.error(err)
		})
	}

	render() {
		if (this.state.albums.length) {
			const albums = this.state.albums.map((album) => {
				return <div key={album.id} className="Albums--item">
					<AlbumCard album={album} />
				</div>
			})
			return <div className="Albums--content">
				{albums}
			</div>
		}
		return <div className="Albums--empty">
			<Jumbotron title="Você ainda não possui albums cadastrados" description="Acesse o menu de cadastro para cadastrar seus albums." />
		</div>
	}
}