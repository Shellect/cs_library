<<<<<<< HEAD
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, throwError } from 'rxjs';
import { User } from '@/shared.types';

@Injectable({
  providedIn: 'root'
})
export class UsersServiceService {

  constructor(public http: HttpClient) { }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.error('An error occurred:', error.error);
    } else {
      console.error(`Backend returned code ${error.status}, body was: `, error.error);
    }
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }

  getUsers() {
    return this.http.get<User[]>("http://localhost:8080/api/v1/user")
      .pipe(catchError(this.handleError));
  }
=======
import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, throwError} from "rxjs";
import {User} from "@/shared.types";
import {environment} from "@/environments/environment";

@Injectable({
    providedIn: 'root'
})
export class UsersServiceService {

    constructor(public http: HttpClient) {
    }

    handleError(error: HttpErrorResponse) {
        if (error.status === 0) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong.
            console.error(`Backend returned code ${error.status}, body was: `, error.error);
        }
        // Return an observable with a user-facing error message.
        return throwError(() => new Error('Something bad happened; please try again later.'));
    }

    getUsers() {
        return this.http.get<User[]>(environment.apiUrl + "/api/v1/user")
            .pipe(catchError(this.handleError))
    }

    getAntiForgeryToken() {
        return this.http.get<string>(environment.apiUrl + "/api/v1/account/antiforgerytoken")
            .pipe(catchError(this.handleError));
    }
>>>>>>> feature/authorisation
}
