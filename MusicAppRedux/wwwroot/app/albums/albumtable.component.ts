import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'albumTable',
    templateUrl: './albumTable.component.html'
})
export class AlbumTableComponent {
    public albums: Album[];

    constructor(http: Http) {
        http.get('/api/AlbumsAPI').subscribe(result => {
            this.albums = result.json();

        });
    }
}

interface Album {
    AlbumID: number;
    name: string;
    artistID: number;
    artist: string;
    genreID: number;
    genre: string;
    rating: number;
    
}