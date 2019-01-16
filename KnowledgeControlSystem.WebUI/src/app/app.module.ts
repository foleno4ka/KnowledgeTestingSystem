import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import 'rxjs/add/operator/map';
import { AppComponent } from './app.component';
import { SignUpComponent } from './user/sign-up/sign-up.component';
import { UserService } from './api/service/user.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { UserComponent } from './user/user.component';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { TestsComponent } from './tests/tests.component';
import { appRoutes } from './routes';
import { AuthGuard } from './auth/auth.guard';
import { TestPageComponent } from './test-page/test-page.component';
import { TestService } from './api/service/test.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { CategoryService } from './api/service/category.service';
import { TestSolveComponent } from './test-solve/test-solve.component';
import { MzCollectionModule, MzRadioButtonModule, MzRadioButtonContainerComponent, MzIconModule, MzCollectionItemComponent } from 'ngx-materialize';
import { MzBadgeModule, MzValidationModule, MzNavbarModule, MzButtonModule, MzSelectModule, MzInputModule, MzCheckboxModule } from 'ngx-materialize';
import { TestResultService } from './api/service/testResult.service';
import { UserListComponent } from './list/userlist.component';
import { StatisticComponent } from './statistic/statistic.component';
import { UserInfoService } from './services/user-info.service';
import { EqualValidator } from './helpers/register.validator';
import { TestStatisticsService } from './api/service/test-statistics.service';

@NgModule({
  declarations: [
    AppComponent,
    SignUpComponent,
    UserComponent,
    SignInComponent,
    TestsComponent,
    TestPageComponent,
    TestSolveComponent,
    UserListComponent,
    StatisticComponent, 
    EqualValidator
  ],
  imports: [
    MzBadgeModule,
    MzValidationModule,
    MzNavbarModule,
    MzButtonModule,
    MzSelectModule,
    MzCheckboxModule,
    MzInputModule,
    MzCollectionModule,
    MzIconModule,
    MzButtonModule,
    MzRadioButtonModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes),
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [UserService, UserInfoService, CategoryService, AuthGuard, TestService, TestResultService, TestStatisticsService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
