import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { TestService } from '../services/test.service';
import { Test } from '../dto/test.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/timer";
import "rxjs/add/operator/finally";
import "rxjs/add/operator/takeUntil";
import "rxjs/add/operator/map";
import { TestResultService } from '../services/testResult.service';
import { TestResult } from '../dto/testResult.model';

@Component({
  selector: 'app-test-solve',
  templateUrl: './test-solve.component.html',
  styleUrls: ['./test-solve.component.css']
})
export class TestSolveComponent implements OnInit {
  test: Test;
  userAnswersMap: { [questionKey: number]: number[] };
  testStartTime: Date;
  testResult: TestResult;

  constructor(private testService: TestService, private testResultService: TestResultService, private route: ActivatedRoute) { }

  ngOnInit() {
    const id: string = this.route.snapshot.params['id'];
    this.testService.seconds = 0;
    this.testService.qnProgress = 0;

    this.userAnswersMap = {};
    this.testService.getTestById(id).subscribe(
      (data: Test) => {
        this.test = data;
        data.Questions.forEach(question => {
          this.userAnswersMap[question.Id] =new Array();
        });
      });

    this.testService.startTest(id).subscribe(
      (data: any) => {
        this.testStartTime = data;
        console.log(this.testStartTime)
      }
    );
  }

  transform(value: number): string {
    const minutes: number = Math.floor(value / 60);
    return ('00' + minutes).slice(-2) + ':' + ('00' + Math.floor(value - minutes * 60)).slice(-2);
  }

  updateAnswers($event: any, questionId: number, answerId: number) {
    if ($event.currentTarget.checked === true) {
      this.userAnswersMap[questionId].push(answerId);
    } else {
      const index: number = this.userAnswersMap[questionId].findIndex(selectedAnswer => selectedAnswer === answerId);
      if (index !== -1) {
        this.userAnswersMap[questionId].splice(index, 1);
      }
    }
  }

  getTestResult() {
    this.testService.finishTest(this.test.Id, this.userAnswersMap).subscribe(
      (data: any) => {
        this.testResult = data;
      })
  }

}
