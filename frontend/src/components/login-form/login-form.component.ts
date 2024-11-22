import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";

@Component({
    selector: 'app-login-form',
    standalone: true,
    imports: [ReactiveFormsModule],
    templateUrl: './login-form.component.html'
})
export class LoginFormComponent {
    loginForm = new FormGroup({
        email: new FormControl(''),
        password: new FormControl('')
    });
    handleSubmit() {

    }
}
