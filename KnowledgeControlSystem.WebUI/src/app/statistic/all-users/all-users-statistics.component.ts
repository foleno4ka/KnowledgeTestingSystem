import { Component, OnInit } from '@angular/core';
import { TestResultService } from '../../api/service/testResult.service';
import { TestStatistic } from '../../api/dto/testStatistic.model';
import { TestService } from '../../api/service/test.service';
import { TestResult } from '../../api/dto/testResult.model';
import { UserInfoService } from '../../services/user-info.service';
import { TestStatisticsService } from '../../api/service/test-statistics.service';

@Component({
  selector: 'all-users-statistics',
  templateUrl: './all-users-statistics.component.html'
})
export class AllUsersStatisticsComponent implements OnInit {

  testStatistic: TestStatistic;
  tempTestResult: TestResult[];
  testResults: TestResult[];
  allUserTestStatistics: TestStatistic[];
  constructor(private testStatisticsService: TestStatisticsService, private testResultService: TestResultService, private testService: TestService, private userInfoservice: UserInfoService) { }

  ngOnInit() {
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
