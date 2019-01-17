import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { TestService } from '../api/service/test.service';
import { Test } from '../api/dto/test.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/timer";
import "rxjs/add/operator/finally";
import "rxjs/add/operator/takeUntil";
import "rxjs/add/operator/map";
import { TestResultService } from '../api/service/testResult.service';
import { TestResult } from '../api/dto/testResult.model';
import { TimerService } from '../services/timer.service';

@Component({
  selector: 'app-test-solve',
  templateUrl: './test-solve.component.html',
  styleUrls: ['./test-solve.component.css']
})
export class TestSolveComponent implements OnInit {
  test: Test;
  userAnswersMap: { [questionKey: number]: number[] };
  testResult: TestResult;

  private SecondsInMinutes: number = 60;
  private MillisecondsInSeconds: number = 1000;

  constructor(private testService: TestService,
    private testResultService: TestResultService,
    private route: ActivatedRoute,
    private timerService: TimerService) { }

  ngOnInit() {
    const id: string = this.route.snapshot.params['id'];

    this.userAnswersMap = {};
    this.testService.getTestById(id).subscribe((data: Test) => {
      this.test = data;
      data.Questions.forEach(question => {
        this.userAnswersMap[question.Id] = new Array();
      });
      this.testService.startTest(id).subscribe((data: string) => {
        let passedMs = new Date().getTime() - new Date(data).getTime();
        let maxDurationMs = this.test.Duration * this.SecondsInMinutes * this.MillisecondsInSeconds;
        let leftMs = maxDurationMs - passedMs;
        if (leftMs < 0) {
          leftMs = 0;
        }
        this.timerService.startTimer(leftMs / this.MillisecondsInSeconds, document.getElementById("testCountdown"));
      });
    });

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
