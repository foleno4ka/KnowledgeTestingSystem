import { Routes } from '@angular/router';
import { TestsComponent } from './tests/tests.component';
import { SignUpComponent } from './user/sign-up/sign-up.component';
import { UserComponent } from './user/user.component';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { AuthGuard } from './auth/auth.guard';
import { TestPageComponent } from './test-page/test-page.component';
import { TestSolveComponent } from './test-solve/test-solve.component';
import { UserListComponent } from './list/userlist.component';
import { StatisticComponent } from './statistic/statistic.component';

export const appRoutes: Routes = [
    { path: 'tests', component: TestsComponent, canActivate: [AuthGuard] },
    {
        path: 'signup', component: UserComponent,
        children: [{ path: '', component: SignUpComponent }]
    },
    {
        path: 'login', component: UserComponent,
        children: [{ path: '', component: SignInComponent }]
    },
    { path: '', redirectTo: '/tests', pathMatch: 'full' },
    { path: 'tests', component: TestsComponent },
    { path: 'tests/:id', component: TestPageComponent },
    { path: 'tests/:id/pass', component: TestSolveComponent },
    { path: 'userlist', component:UserListComponent},
    { path: 'statistic', component:StatisticComponent}
];