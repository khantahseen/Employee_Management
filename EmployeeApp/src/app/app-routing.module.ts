import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddDepartmentComponent } from './add-department/add-department.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { DepartmentListComponent } from './department-list/department-list.component';
import { EditDepartmentComponent } from './edit-department/edit-department.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { LoginUserComponent } from './login-user/login-user.component';
import {LoginGuard} from './Authentication/login.guard';
import { DepGuardGuard } from './Authentication/DepartmentGuard/dep-guard.guard';
import { EmpGuard } from './Authentication/EmployeeActionsGuard/emp.guard';
import { NoAccessComponent } from './shared/no-access/no-access.component';

const routes: Routes = [
  { path: 'empList', component: EmployeeListComponent, canActivate:[LoginGuard]},
  { path: 'deptList', component: DepartmentListComponent,canActivate:[EmpGuard]},
  { path:  'loginForm', component:LoginUserComponent},
  { path: 'addEmp', component:AddEmployeeComponent,canActivate:[EmpGuard]},
  { path: 'empList/editEmp/:id', component: EditEmployeeComponent },
  { path: 'deptList/editDep/:id', component: EditDepartmentComponent},
  { path: 'addDep', component:AddDepartmentComponent,canActivate:[DepGuardGuard]},
  {path:'noAccess', component:NoAccessComponent},
  { path:'', redirectTo: '/loginForm', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
