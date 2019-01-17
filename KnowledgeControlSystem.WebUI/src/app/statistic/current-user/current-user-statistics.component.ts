import { Component, OnInit } from '@angular/core';
import { TestResultService } from '../../api/service/testResult.service';
import { TestStatistic } from '../../api/dto/testStatistic.model';
import { TestService } from '../../api/service/test.service';
import { TestResult } from '../../api/dto/testResult.model';
import { UserInfoService } from '../../services/user-info.service';
import { TestStatisticsService } from '../../api/service/test-statistics.service';

@Component({
  selector: 'current-user-statistics',
  templateUrl: './current-user-statistics.component.html'
})
export class CurrentUserStatisticsComponent implements OnInit {

  testStatistic: TestStatistic;
  tempTestResult: TestResult[];
  testResults: TestResult[];
  allUserTestStatistics: TestStatistic[];
  constructor(private testStatisticsService: TestStatisticsService, private testResultService: TestResultService, private testService: TestService, private userInfoservice: UserInfoService) { }

  ngOnInit() {
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

}
