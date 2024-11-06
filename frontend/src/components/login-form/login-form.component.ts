import {Component} from '@angular/core';
import {environment} from "@/environments/environment";

@Component({
    selector: 'app-login-form',
    standalone: true,
    imports: [],
    templateUrl: './login-form.component.html'
})
export class LoginFormComponent {
    apiUrl: string = environment.apiUrl;
}