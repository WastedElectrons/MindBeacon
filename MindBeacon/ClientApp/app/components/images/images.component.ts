import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'images',
    templateUrl: './images.component.html'
})
export class ImagesComponent {
    public images: Image[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Image').subscribe(result => {
            this.images = result.json() as Image[];
        }, error => console.error(error));
    }
}

interface Image {
    Id: number;
    name: string;
    createdAt: Date;
    imageUrl: string;
}
