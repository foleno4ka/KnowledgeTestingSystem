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
export class TestService {
  readonly rootUrl = 'http://localhost:56607/api/Tests';
  seconds: number;
  timer;
  qnProgress: number;
  correctAnswerCount: number = 0;
  constructor(private http: HttpClient) { }

  createTest(test: Test): Observable<Test> {
    return this.http.post<Test>(this.rootUrl, test, this.getOptions()).pipe(
      tap((test: Test) => console.log(`added test w/ id=${test.Id}`)),
      catchError(this.handleError<any>('addTest'))
    );
  }

  getTestById(id: string): Observable<Test> {
    return this.http.get<Test>(`${this.rootUrl}/${id}`, this.getOptions()).pipe();
  }

  deleteTest(id: number) {
    return this.http.delete(`${this.rootUrl}/${id}`, this.getOptions()).pipe(
      tap(_ => console.log(`deleted test id=${id}`)),
      catchError(this.handleError<Test>(`deleteTest`)));
  }

  updateTest(id: number, test: Test): Observable<Test> {
    return this.http.put<Test>(`${this.rootUrl}/${id}`, test, this.getOptions()).pipe(
      tap(_ => console.log(`updated test id=${id}`)),
      catchError(this.handleError<any>('updateTest'))
    );
  }

  getTestsName(): Observable<string[]> {
    return this.http.get<string[]>(`${this.rootUrl}/TestNames`, this.getOptions()).pipe(
      tap(test => console.log('fetched TestNames')),
      catchError(this.handleError('getTestNames', [])));
  }

  getTests(): Observable<Test[]> {
    return this.http.get<Test[]>(`${this.rootUrl}`, this.getOptions()).pipe(
      tap(test => console.log('fetched TestNames')),
      catchError(this.handleError('getTestNames', [])));
  }

  getTestByName(testName: string): Observable<Test> {
    return this.http.get<Test>(`${this.rootUrl}/${testName}`, this.getOptions()).pipe();
  }

  getTestsByCategoryName(categoryName: string): Observable<Test[]> {
    return this.http.get<Test[]>(`${this.rootUrl}/${categoryName}/Tests`, this.getOptions()).pipe(
      tap(test => console.log('fetched TestNames')),
      catchError(this.handleError('getTestNames', [])));
  }

  searchTestName(testName: string): Observable<string[]> {
    if (!testName.trim())
      return of([]);
    return this.http.get<string[]>(`${this.rootUrl}/{testName}`, this.getOptions()).pipe(
      tap(test => console.log(`found heroes matching "${testName}"`)),
      catchError(this.handleError('searchTestNames', [])));

  }

  displayTimeElapsed() {
    return Math.floor(this.seconds / 3600) + ':' + Math.floor(this.seconds / 60) + ':' + Math.floor(this.seconds % 60);
  }

  startTest(testId: string) {
    return this.http.post(`${this.rootUrl}/${testId}/Start`, null, this.getOptions()).pipe(
      tap((test: Test) => console.log(`added test w/ id=${testId}`)),
      catchError(this.handleError('addTest'))
    );
  }

  finishTest(testId: number, userAnswersMap: { [questionKey: number]: number[] }) {
    return this.http.post(`${this.rootUrl}/${testId}/Finish`, userAnswersMap, this.getOptions()).pipe(
      tap((testResult: TestResult) => console.log(`finished test id=${testId}`)),
      catchError(this.handleError('finished Test'))
    );
  }
  // getTestResultStatics() {
  //   return this.http.get(`${this.rootUrl}/testStatistics`, this.getOptions()).pipe(
  //     tap((userStatistic: TestStatistic) => console.log(`calculated testStatistic`)),
  //     catchError(this.handleError('calculated statistics')));
  // }

  public getTestResultStatics() {
    return this.http.get(`http://localhost:56607/api/TestStatistics`, this.getOptions()).pipe()
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    }
  }

  private getOptions() {
    const header = new HttpHeaders().set('Authorization', 'Bearer' + localStorage.getItem('userToken'));
    return {
      headers: header
    };
  }

}
