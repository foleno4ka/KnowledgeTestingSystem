import { Component, OnInit } from '@angular/core';
import { TestResultService } from '../services/testResult.service';
import { TestStatistic } from '../dto/testStatistic.model';
import { TestService } from '../services/test.service';
import { TestResult } from '../dto/testResult.model';
import { UserInfoService } from '../shared/user-info.service';
import { TestStatisticsService } from '../services/test-statistics.service';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css']
})
export class StatisticComponent implements OnInit {

  testStatistic: TestStatistic;
  tempTestResult: TestResult[];
  testResults: TestResult[];
  allUserTestStatistics: TestStatistic[];
  constructor(private testStatisticsService: TestStatisticsService, private testResultService: TestResultService, private testService: TestService, private userInfoservice: UserInfoService) { }

  ngOnInit() {
    if (!this.userInfoservice.isInRole(['Admin', 'Moderator'])) {
      this.testStatisticsService.getTestResultStatics().subscribe((data: any) => {
        this.testStatistic = data;
        if (this.testStatistic === null) {
          this.testStatistic.PassedTestCount = 0;
          this.testStatistic.AvgScorePercent = 0;
          this.testStatistic.AvgTimeSeconds = '0';
        }
      });

      this.testResultService.getTestResults().subscribe((data: TestResult[]) => {
        if (data !== null) {
          this.tempTestResult = data;
          this.tempTestResult.forEach(testResult => {
            var minutes = Math.floor((new Date(testResult.EndTime).valueOf() - new Date(testResult.StartTime).valueOf()) / (60 * 1000));
            var seconds = ((new Date(testResult.EndTime).valueOf() - new Date(testResult.StartTime).valueOf()) % (60000) / 1000).toFixed(0);
            testResult.Duration = minutes + ":" + (Number.parseInt(seconds) < 10 ? "0" : "") + seconds;
            testResult.PassTime = new Date(testResult.EndTime).toLocaleTimeString();
            testResult.EndTime = new Date(testResult.EndTime).toDateString();
          })
          this.testResults = this.tempTestResult;
        }
      });
    }
    else {
      this.getAllUserStatistic();
    }
  }

  getAllUserStatistic() {
    this.testStatisticsService.getAllTestResultStatics().subscribe((data: TestStatistic[]) => {
      this.allUserTestStatistics = data;
      this.allUserTestStatistics.forEach(testStatistic => {
        var minutes = Math.floor(Number.parseInt(testStatistic.AvgTimeSeconds) / (60 * 1000));
        var seconds = (Number.parseInt(testStatistic.AvgTimeSeconds) % (60)).toFixed(0);
        testStatistic.AvgTimeSeconds = minutes + ":" + (Number.parseInt(seconds) < 10 ? "0" : "") + seconds;
      })
    })
  }

}
