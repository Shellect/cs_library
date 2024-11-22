import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import { environment } from "@/environments/environment";
import { catchError, throwError } from 'rxjs';
import { AccountResponse } from '@/shared.types';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private accessToken?: string;

    constructor(public http: HttpClient, private router: Router) {
    }

    handleError(err: HttpErrorResponse) {
        return throwError(() => err)
    }

    login(email: string, password: string) {
        this.http
        .post<AccountResponse>(environment.apiUrl + '/account/login', {email, password})
        .pipe(catchError(this.handleError))
        .subscribe(response => this.accessToken = response.accessToken)
    }
}