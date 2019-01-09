import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { TestResult } from '../dto/testResult.model';
import { catchError, tap } from 'rxjs/operators';

@Injectable()
export class TestResultService {
    readonly rootUrl = 'http://localhost:56607/api/TestResults';
    //'http://localhost:56607/api/Tests'

    constructor(private http: HttpClient) { }

    createTestResult(testResultId: number, userAnswers: number[]): Observable<TestResult> {
        const header = new HttpHeaders().set('Authorization', 'Bearer' + localStorage.getItem('userToken'));
        return this.http.post<TestResult>(`${this.rootUrl}/${testResultId}`, userAnswers, { headers: header }).pipe();
    }


    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
        }
    }
}
