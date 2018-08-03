import React from 'react'

import Button from '../../../../components/generic/Button/Button'

import './AlbumCard.css'

export default class AlbumCard extends React.Component {

    render() {
        return <div className="card">
            <div className="card-body">
                <h5 className="card-title">{this.props.album.nome}</h5>
                <ul>
                    {this.props.album.musicas.map(t => <li key={t.id}>{t.nome}</li>)}
                </ul>
            </div>
        </div>
    }
}