import {Routes} from '@angular/router';
import {DetailsComponent} from "@/components/details/details.component";
import {LoginFormComponent} from "@/components/login-form/login-form.component";
import { RegisterFormComponent } from '@/components/register-form/register-form.component';
import { NotFoundPageComponent } from '@/components/not-found-page/notFoudPage.component';

export const routes: Routes = [
    {path: '', component: DetailsComponent},
    {path: 'register', component: RegisterFormComponent},
    {path: 'login', component: LoginFormComponent},
    {path: '**', component: NotFoundPageComponent}
];
