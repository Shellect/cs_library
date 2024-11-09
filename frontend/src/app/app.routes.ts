import { Routes } from '@angular/router';
import {DashboardComponent} from "@/components/dashboard/dashboard.component"
import { LoginFormComponent } from '@/components/login-form/login-form.component';

export const routes: Routes = [
    {"path": "login", "component" : LoginFormComponent},
    {"path": "**", "component": DashboardComponent}
];
