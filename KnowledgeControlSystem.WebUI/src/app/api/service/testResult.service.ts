import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { TestResult } from '../dto/testResult.model';
import { catchError, tap } from 'rxjs/operators';
import { GenericApiService } from './generic-api.service';

@Injectable()
export class TestResultService extends GenericApiService {

    constructor(private http: HttpClient) {
        super();
    }

    getTestResults(): Observable<TestResult[]> {
        return this.http.get<TestResult[]>(`${this.rootUrl}/api/TestResults`, this.getOptions());
    }

}
