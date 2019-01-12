import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http/src/response';
import { Router } from '@angular/router';
import { UserService } from '../../shared/user.service';
import { UserInfoService } from '../../shared/user-info.service';
import { TokenDto } from '../../dto/token-dto.model';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  isLoginError: boolean = false;
  constructor(private userService: UserService, private userInfoService: UserInfoService, private router: Router) { }

  ngOnInit() {
  }
  OnSubmit(userName, password) {
    this.userService.userAuthentication(userName, password).subscribe((data: TokenDto) => {
      this.userInfoService.setInfo(data);
      this.router.navigate(['/tests']);
    }, (err: HttpErrorResponse) => { this.isLoginError = true });
  }
}
