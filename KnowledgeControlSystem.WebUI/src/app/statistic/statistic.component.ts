import { Component, OnInit } from '@angular/core';
import { TestResultService } from '../services/testResult.service';
import { TestStatistic } from '../dto/testStatistic.model';
import { TestService } from '../services/test.service';
import { TestResult } from '../dto/testResult.model';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css']
})
export class StatisticComponent implements OnInit {

  testStatistic: TestStatistic;
  testResults: TestResult[];
  constructor(private testResultService: TestResultService, private testService: TestService) { }

  ngOnInit() {
    this.testService.getTestResultStatics().subscribe((data: any) => {
      this.testStatistic = data;
    });
    this.testResultService.getTestResults().subscribe((data: TestResult[]) => {
      this.testResults = data;
    });
  }

}
