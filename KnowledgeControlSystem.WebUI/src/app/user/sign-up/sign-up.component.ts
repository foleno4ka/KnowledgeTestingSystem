import { Component, OnInit } from '@angular/core';
import { User } from '../../api/dto/user.model';
import { NgForm, Validators } from '@angular/forms';
import { UserService } from '../../api/service/user.service';
import { ToastrService } from 'ngx-toastr'
import { EqualValidator } from '../../helpers/register.validator';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  user: User;
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  constructor(private userService: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.user={
      Id:undefined,
      UserName: '',
      Password: '',
      Email: '',
      ConfirmPassword:'',
      Roles: []
    }
  }

  OnSubmit(form: NgForm) {
    this.userService.registerUser(form.value)
      .subscribe((data: any) => {
        if (data.Succeeded == true) {
          this.resetForm(form);
          this.toastr.success('User registration successful');
        }
        else
          this.toastr.error(data.Errors[0]);
      });
  }

}
