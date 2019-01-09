import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import {  Response } from "@angular/http";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/filter';
import { Test } from '../dto/test.model';
import { Body } from '@angular/http/src/body';
import { catchError, tap, map } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { Category } from '../dto/category.model';

@Injectable()
export class CategoryService {
  readonly rootUrl = 'http://localhost:56607/api/Categories';
  constructor(private http: HttpClient) { }


  getCategories():Observable<Category[]>{
    const header = new HttpHeaders().set('Authorization', 'Bearer'+localStorage.getItem('userToken'));
        return this.http.get<Category[]>(this.rootUrl, {headers:header});         
  }
  private handleError<T> (operation='operation', result?:T){
    return (error:any):Observable<T>=>{
        console.error(error);
        return of(result as T);
      }
    }
}