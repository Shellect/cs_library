import {Routes} from '@angular/router';
import {DetailsComponent} from "@/components/details/details.component";
import {LoginFormComponent} from "@/components/login-form/login-form.component";

export const routes: Routes = [
    {path: 'login', component: LoginFormComponent},
    {path: '**', component: DetailsComponent}
];
