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
}
