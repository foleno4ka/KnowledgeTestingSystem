import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Response } from "@angular/http";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/filter';
import { Test } from '../dto/test.model';
import { Body } from '@angular/http/src/body';
import { catchError, tap, map } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { Category } from '../dto/category.model';
import { GenericApiService } from './generic-api.service';

@Injectable()
export class CategoryService extends GenericApiService {

  constructor(private http: HttpClient) {
    super();
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.rootUrl}/api/Categories`, this.getOptions());
  }

}