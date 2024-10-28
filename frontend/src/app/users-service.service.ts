import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { UsersResponse } from './user';

@Injectable({
  providedIn: 'root'
})
export class UsersServiceService {

  constructor(public http: HttpClient) { }

  getUsers() {
    return this.http.get<UsersResponse>("/api/v1/user");
  }
}
