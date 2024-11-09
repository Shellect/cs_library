import { Component } from '@angular/core';
import { UsersServiceService } from '@/services/users-service.service';
import { User } from '@/shared.types';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent {
  users: User[] = [];
  constructor(private usersService: UsersServiceService) {}

  ngOnInit () {
    this.usersService.getUsers().subscribe(users => this.users = users);
  }
}
