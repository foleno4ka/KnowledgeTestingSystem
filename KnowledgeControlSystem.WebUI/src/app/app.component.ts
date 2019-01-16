import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from './services/user-info.service';


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'app';

    constructor(private router: Router, private userInfoService: UserInfoService) { }

    logout() {
        localStorage.clear();
        this.router.navigate(['/login']);
    }

}