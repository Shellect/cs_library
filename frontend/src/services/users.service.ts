import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, throwError } from "rxjs";
import { User } from "@/shared.types";
import { environment } from "@/environments/environment";
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class UsersService {

    constructor(public http: HttpClient, private router: Router) {
    }

    handleError(error: HttpErrorResponse) {
        if ([401, 403].includes(error.status)) {
            this.router.navigateByUrl('/login');
        }
        return throwError(() => error)
    }

    getUsers() {
        return this.http.get<User[]>(environment.apiUrl + "/user")
            .pipe(catchError(this.handleError))
    }
}
