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
export class TestService extends GenericApiService {
  constructor(private http: HttpClient) {
    super();
  }

  createTest(test: Test): Observable<Test> {
    return this.http.post<Test>(`${this.rootUrl}/api/Tests`, test, this.getOptions());
  }

  getTestById(id: string): Observable<Test> {
    return this.http.get<Test>(`${this.rootUrl}/api/Tests/${id}`, this.getOptions());
  }

  deleteTest(id: number) {
    return this.http.delete(`${this.rootUrl}/api/Tests/${id}`, this.getOptions());
  }

  updateTest(id: number, test: Test): Observable<Test> {
    return this.http.put<Test>(`${this.rootUrl}/api/Tests/${id}`, test, this.getOptions());
  }

  getTests(): Observable<Test[]> {
    return this.http.get<Test[]>(`${this.rootUrl}/api/Tests`, this.getOptions());
  }

  startTest(testId: string): Observable<string> {
    return this.http.post<string>(`${this.rootUrl}/api/Tests/${testId}/Start`, null, this.getOptions());
  }

  finishTest(testId: number, userAnswersMap: { [questionKey: number]: number[] }) {
    return this.http.post(`${this.rootUrl}/api/Tests/${testId}/Finish`, userAnswersMap, this.getOptions());
  }

}
