import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { DataInterceptor } from './Shared/data.interceptor';
import {LoginGuard} from './Authentication/login.guard';
import {DepGuardGuard} from './Authentication/DepartmentGuard/dep-guard.guard';
import {EmpGuard} from './Authentication/EmployeeActionsGuard/emp.guard';



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { DepartmentListComponent } from './department-list/department-list.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { AddDepartmentComponent } from './add-department/add-department.component';
import { EditDepartmentComponent } from './edit-department/edit-department.component';
import { LoginUserComponent } from './login-user/login-user.component';
import { NoAccessComponent } from './shared/no-access/no-access.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent,
    DepartmentListComponent,
    AddEmployeeComponent,
    EditEmployeeComponent,
    AddDepartmentComponent,
    EditDepartmentComponent,
    LoginUserComponent,
    NoAccessComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [LoginGuard,DepGuardGuard,EmpGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: DataInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
