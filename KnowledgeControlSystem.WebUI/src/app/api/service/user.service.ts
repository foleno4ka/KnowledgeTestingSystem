import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from "@angular/http";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../dto/user.model';
import { Router } from '@angular/router';
import { NO_AUTH_HEADER } from '../../auth/auth.interceptor';
import { TokenDto } from '../dto/token-dto.model';
import { GenericApiService } from './generic-api.service';

@Injectable()
export class UserService extends GenericApiService {

    constructor(private http: HttpClient, private router: Router) {
        super();
    }

    registerUser(user: User) {
        const body = {
            UserName: user.UserName,
            Password: user.Password,
            Email: user.Email,
        }
        var reqHeader = new HttpHeaders({ [NO_AUTH_HEADER]: 'True' });
        return this.http.post(this.rootUrl + '/api/Users', body, { headers: reqHeader });
    }

    userAuthentication(userName, password): Observable<TokenDto> {
        var data = "username=" + userName + "&password=" + password + "&grant_type=password";
        var requestHeader = new HttpHeaders(
            {
                'Content-Type': 'application/x-www-form-urlencoded',
                [NO_AUTH_HEADER]: 'True'
            });
        return this.http.post<TokenDto>(this.rootUrl + '/token', data, { headers: requestHeader });
    }

    getAllRoles() {
        return this.http.get(this.rootUrl + '/api/Roles', this.getOptions()).toPromise();
    }

    getUsers() {
        return this.http.get(`${this.rootUrl}/api/Users`, this.getOptions()).toPromise();
    }

    getUser(id) {
        return this.http.get(`${this.rootUrl}/api/Users/${id}`).toPromise();
    }

    delete(id) {
        return this.http.delete(`${this.rootUrl}/api/Users/${id}/`).toPromise();
    }

    create(user) {
        return this.http.post(`${this.rootUrl}/api/Users/`, user).toPromise();
    }

    update(user: User) {
        return this.http.put(`${this.rootUrl}/api/Users/${user.Id}/`, user).toPromise();
    }

}
