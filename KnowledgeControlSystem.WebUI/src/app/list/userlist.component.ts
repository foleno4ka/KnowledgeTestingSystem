import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';
import { User } from '../dto/user.model';
import { subscribeOn } from 'rxjs/operators';

@Component({
  selector: 'userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserListComponent implements OnInit {
  users: Array<User>
  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.userService.getUsers().then((data: any) => {
      this.users = data as User[];
    })
    // this.users.forEach(user=>{
    //   this.userService.de
    // })
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
}

