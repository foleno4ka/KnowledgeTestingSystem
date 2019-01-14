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

// const httpOptions = {
//   headers: new HttpHeaders({ 'Content-Type': 'application/json' })
// };

@Injectable()
export class TestStatisticsService {
    readonly rootUrl = 'http://localhost:56607/api';
    seconds: number;
    timer;
    qnProgress: number;
    correctAnswerCount: number = 0;
    constructor(private http: HttpClient) { }

    public getTestResultStatics() {
        return this.http.get(`${this.rootUrl}/TestStatistics`, this.getOptions()).pipe()
    }

    public getAllTestResultStatics(): Observable<TestStatistic[]> {
        return this.http.get<TestStatistic[]>(`${this.rootUrl}/AllTestStatistics`, this.getOptions()).pipe()
    }

    private getOptions() {
        const header = new HttpHeaders().set('Authorization', 'Bearer' + localStorage.getItem('userToken'));
        return {
            headers: header
        };
    }

}
