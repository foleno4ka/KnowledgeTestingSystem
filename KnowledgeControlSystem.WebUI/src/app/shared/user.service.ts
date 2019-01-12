import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from "@angular/http";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../dto/user.model';
import { Router } from '@angular/router';
import { NO_AUTH_HEADER } from '../auth/auth.interceptor';
import { TokenDto } from '../dto/token-dto.model';

@Injectable()
export class UserService {
    readonly rootUrl = 'http://localhost:56607';
    constructor(private http: HttpClient, private router: Router) { }

    registerUser(user: User, roles: string[]) {
        const body = {
            UserName: user.UserName,
            Password: user.Password,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Roles: roles
        }
        var reqHeader = new HttpHeaders({ [NO_AUTH_HEADER]: 'True' });
        return this.http.post(this.rootUrl + '/api/User/Register', body, { headers: reqHeader });
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

    getCurrentUser(): Observable<User> {
        return this.http.get<User>(this.rootUrl + '/api/CurrentUser');
    }

    getAllRoles() {
        var header = new HttpHeaders({ 'No-Auth': 'True' });
        return this.http.get(this.rootUrl + '/api/GetAllRoles', { headers: header });
    }

    private getOptions() {
        const header = new HttpHeaders({
            'Authorization': 'Bearer' + localStorage.getItem('userToken'),
            'Content-Type': 'application/json'
        });
        return {
            headers: header
        };
    }
    getUsers() {
        return this.http.get(`${this.rootUrl}/api/Users`, this.getOptions())
            .map(response => response)
            .toPromise();
    }

    getUser(id) {
        const url = `${this.rootUrl}/api/Users${id}`;
        return this.http.get(url)
            .toPromise()
            .then(response => JSON.stringify(response))
            .catch(this.handleError);
    }
    delete(id) {
        const url = `${this.rootUrl}/api/Users/${id}/`;
        return this.http.delete(url)
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }
    create(user) {
        return this.http
            .post(`${this.rootUrl}/api/Users/s/`, user)
            .toPromise()
            .then(res => JSON.stringify(res))
            .catch(this.handleError);
    }
    update(user) {
        const url = `${this.rootUrl}/api/Users/${user._id}/`;
        return this.http
            .put(url, user)
            .toPromise()
            .then(() => user)
            .catch(this.handleError);
    }
    changePassword(data) {
        return this.http.post(`${this.rootUrl}/change_password/`, data)
            .toPromise()
            .then((response) => {
                return JSON.stringify(response);
            })
            .catch(this.handleError);
    }

    handleError(error) {
        return Promise.reject(error.message || error);
    }
}
