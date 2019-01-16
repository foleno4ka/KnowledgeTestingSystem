import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Test } from '../dto/test.model';
import { Body } from '@angular/http/src/body';
import { catchError, tap, map } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { TestStatistic } from '../dto/testStatistic.model';
import { TestResult } from '../dto/testResult.model';
import { GenericApiService } from './generic-api.service';

@Injectable()
export class TestStatisticsService extends GenericApiService {

    constructor(private http: HttpClient) {
        super();
    }

    public getTestResultStatics() {
        return this.http.get(`${this.rootUrl}/api/TestStatistics`, this.getOptions());
    }

    public getAllTestResultStatics(): Observable<TestStatistic[]> {
        return this.http.get<TestStatistic[]>(`${this.rootUrl}/api/AllTestStatistics`, this.getOptions());
    }

}
