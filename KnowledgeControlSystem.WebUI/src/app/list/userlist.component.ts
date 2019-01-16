import { Component, OnInit } from '@angular/core';
import { UserService } from '../api/service/user.service';
import { Router } from '@angular/router';
import { User } from '../api/dto/user.model';
import { subscribeOn } from 'rxjs/operators';
import { Role } from '../api/dto/role.model';

@Component({
  selector: 'userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
  roles: string[];
  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.userService.getAllRoles().then((data: Role[]) => {
      this.roles = data.map((role) => role.Name);
    });
    this.userService.getUsers().then((data: any) => {
      this.users = data;
    });
  }

  deleteUser(idToDelete: number) {
    this.userService.delete(idToDelete).then(() => {
      let indexToRemove = -1;
      this.users.forEach((user, index) => {
        if (user.Id === idToDelete) {
          indexToRemove = index;
        }
      });
      if (indexToRemove !== -1) {
        this.users.splice(indexToRemove, 1);
      }
    });
  }

  hasRole(user: User, role: string) {
    return user.Roles.indexOf(role) !== -1;
  }

  toggleRole(user: User, role: string) {
    if (this.hasRole(user, role)) {
      this.userService.removeRole(user.Id, role).then(() => {
        let index = user.Roles.indexOf(role);
        if (index !== -1)
          user.Roles.splice(index, 1);
      });
    } else {
      this.userService.addRole(user.Id, role).then(() => {
        user.Roles.push(role);
      });
    }
  }

}

