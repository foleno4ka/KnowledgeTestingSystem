import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { User } from '../dto/user.model';
import { TestService } from '../services/test.service';
import { Test } from '../dto/test.model';
import { UserInfoService } from '../shared/user-info.service';

@Component({
  selector: 'tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.css']
})
export class TestsComponent implements OnInit {
  tests: Test[];

  constructor(private router: Router, private userInfoService: UserInfoService, private testService: TestService) { }

  ngOnInit() {
    this.testService.getTests().subscribe(data => {
      this.tests = data
    });
  }

  deleteTest(idToDelete: number) {
    this.testService.deleteTest(idToDelete).subscribe(() => {
      let indexToRemove = -1;
      this.tests.forEach((test, index) => {
        if (test.Id === idToDelete) {
          indexToRemove = index;
        }
      });
      if (indexToRemove !== -1) {
        this.tests.splice(indexToRemove, 1);
      }
    });
  }

}