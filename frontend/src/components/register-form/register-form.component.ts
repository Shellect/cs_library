import {Component} from '@angular/core';
import {environment} from "@/environments/environment";

@Component({
    selector: 'app-register-form',
    standalone: true,
    templateUrl: './register-form.component.html'
})
export class RegisterFormComponent {
    apiUrl: string = environment.apiUrl;
}
