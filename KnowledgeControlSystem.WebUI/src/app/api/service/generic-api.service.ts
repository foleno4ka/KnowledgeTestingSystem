import { HttpHeaders } from "@angular/common/http";

export abstract class GenericApiService {
    protected readonly rootUrl = 'http://localhost:56607';

    protected getOptions() {
        const header = new HttpHeaders({
            'Authorization': 'Bearer' + localStorage.getItem('userToken'),
            'Content-Type': 'application/json'
        });
        return {
            headers: header
        };
    }

}