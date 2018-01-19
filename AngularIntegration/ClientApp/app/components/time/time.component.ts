import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'time',
    templateUrl: './time.component.html'
})
export class TimeComponent {

    currentTime: string;

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {

        this.refreshTime();
    }

    refreshTime() {

        this
            .http
            .get(`${this.baseUrl}api/time`)
            .subscribe(
            result => {
                console.log(result);
                console.log(result.json());

                this.currentTime = result.json().formatted;
            },
                error => console.error(error)
        );
    }
}
