import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { employee } from '../Models/Employee';
import { department } from '../Models/Department'
import {login} from '../Models/Login';
import { catchError, tap, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class DataService {

  private eurl = 'https://localhost:44318/api/EmployeesApi';
  private durl = 'https://localhost:44318/api/DepartmentsApi';
  private aurl = 'https://localhost:44318/api/AccountsApi/login';

  constructor(private http: HttpClient, private route:Router) { }
  
  emp:employee;
  emplist:Observable<employee[]>;

  loginUser(l:login):void{
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
   // headers.append('Authorization','Bearer '+localStorage.getItem('userToken'))
    const as=JSON.stringify(l);
    console.log(as);
    this.http.post(this.aurl,as,{headers: headers}).subscribe((data:any)=>{
      localStorage.setItem('userToken',data.token);
      console.log(data.token);
      let tk=localStorage.getItem('userToken');
      let jwtData = tk.split('.')[1]
      let decodedJwtJsonData = window.atob(jwtData);
      let decodedJwtData = JSON.parse(decodedJwtJsonData);
      let isRole = decodedJwtData.role;
      localStorage.setItem('userRole',isRole);
      localStorage.setItem('userName',decodedJwtData.name);
      console.log('LocalRole:'+localStorage.getItem('userRole'));
      console.log('jwtData: ' + jwtData);
      console.log('decodedJwtJsonData: ' + decodedJwtJsonData);
      console.log('decodedJwtData: ' + decodedJwtData);
      console.log('Is admin: ' + isRole);
      //console.log('Name:' + decodedJwtData.name);
    });
      //(c=>{console.log(c)});
    this.route.navigate(['/empList']);
    }

    checkRoleAdmin(): boolean {
      if (localStorage.getItem('userRole') == 'Admin') {
        return true;
      } else {
        return false;
      }
    }
    checkRoleHR(): boolean {
      if (localStorage.getItem('userRole') == 'HR') {
        return true;
      } else {
        return false;
      }
    }

    checkRoleEmployee(): boolean {
      if (localStorage.getItem('userRole') == 'Employee') {
        return true;
      } else {
        return false;
      }
    }

  getEmployees():Observable<employee[]>{
    return this.http.get<employee[]>(this.eurl).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  getEmployee(id:number):Observable<employee>{

    return this.http.get<employee>(this.eurl + `/${id}`).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  addEmployee(e:employee):void{
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    const as=JSON.stringify(e);
    console.log(as);
    this.http.post(this.eurl,as,{headers: headers}).subscribe(
      ()=>{
        return this.getEmployees();
      }
    );
    this.route.navigate(['/empList']);
    }

    deleteEmployee(id:number):void{
      if(confirm("Delete this employee?")){
        const deleteurl=`https://localhost:44318/api/EmployeesApi/${id}`;
        this.http.delete(deleteurl,{ responseType: 'text' }).subscribe(
          ()=>{
            return this.getEmployees();
          }
        );
      }
    }
    
    getDepartments():Observable<department[]>{
    return this.http.get<department[]>(this.durl).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  getDepartment(id:number):Observable<department>{

    return this.http.get<department>(this.durl + `/${id}`).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError)
    );
  }
  
  addDepartment(d:department):void{
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    //headers.append('Authorization','Bearer '+localStorage.getItem('userToken'))
    const as=JSON.stringify(d);
    console.log(as);
    this.http.post(this.durl,as,{headers: headers}).subscribe(
      ()=>{
        return this.getDepartments();
      }
    );
    //this.route.navigate(['/deptList']);
  }

  deleteDepartment(id:number):void{
    if(confirm("Delete this Department?")){
      const deletedurl=`https://localhost:44318/api/DepartmentsApi/${id}`;
      this.http.delete(deletedurl,{ responseType: 'text' }).subscribe(
        ()=>{
          return this.getDepartments();
        }
      );
    }
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    //console.error(errorMessage);
    return throwError(errorMessage);
  }
}
