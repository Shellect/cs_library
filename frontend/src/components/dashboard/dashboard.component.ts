import { Component } from '@angular/core';
import { UsersService } from '@/services/users.service';
import { User } from '@/shared.types';
import { Subscription, throwError } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent {
  users: User[] = [];
  subscription: Subscription | null = null;
  constructor(private usersService: UsersService) { }

  ngOnInit() {
    this.subscription = this.usersService.getUsers().subscribe(users => this.users = users);
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
