import { Component, OnInit } from '@angular/core';
import { User } from '../../dto/user.model';
import { NgForm } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr'

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  user: User;
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  roles: any[];
  constructor(private userService: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.resetForm();

    this.userService.getAllRoles().subscribe(
      (data: any) => {
        data.forEach(role => { role.selected = false });
        this.roles = data;
      })
  }

  resetForm(form?: NgForm) {
    if (this.roles)
      this.roles.map(x => x.selected = false);
    if (form != null)
      form.reset();
    this.user={
      Id:undefined,
      UserName: '',
      Password: '',
      Email: '',
      FirstName: '',
      LastName: '',
      UserRoles:this.roles
    }
  }

  OnSubmit(form: NgForm) {
    var currectRoles = this.roles.filter(x => x.selected).map(y => y.Name);
    this.userService.registerUser(form.value, currectRoles)
      .subscribe((data: any) => {
        if (data.Succeeded == true) {
          this.resetForm(form);
          this.toastr.success('User registration successful');
        }
        else
          this.toastr.error(data.Errors[0]);
      });
  }

  updateSelectedRoles(index) {
    this.roles[index].selected = !this.roles[index].selected;
  }

}
