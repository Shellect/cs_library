import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UsersServiceService } from './users-service.service';
import { User, UsersResponse } from './user';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'clientApp';
  users: User[] = [];
  constructor(private usersService: UsersServiceService) {}

  ngOnInit () {
    this.usersService.getUsers().subscribe(usersResponse => this.users = usersResponse.result);
  }
}
