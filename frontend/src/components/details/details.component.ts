import { Component } from '@angular/core';
import {UsersServiceService} from "@/services/users-service.service";
import {User} from "@/shared.types";

@Component({
  selector: 'app-details',
  standalone: true,
  imports: [],
  templateUrl: './details.component.html'
})
export class DetailsComponent {
  users: User[] = [];
  constructor(private usersService: UsersServiceService) {}

  ngOnInit () {
    this.usersService.getUsers().subscribe(users => this.users = users);
  }
}
