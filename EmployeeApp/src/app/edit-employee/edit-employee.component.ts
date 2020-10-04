import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { department } from '../Models/Department';
import { employee } from '../Models/Employee';
import { DataService } from '../Shared/data.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  constructor(private edata:DataService,private route:Router,private ar:ActivatedRoute, private http:HttpClient) { 
  }
  id:number=0;
  url:string='https://localhost:44318/api/EmployeesApi';
  department:department[];
  employeeValue:employee={
    EmployeeId:this.id,
    Name:null,
    Surname:null,
    Email:null,
    Address:null,
    Qualification:null,
    ContactNumber:0,
    DepartmentID:0,
    department:null
  };

  ngOnInit(): void {
    this.ar.params.subscribe(params =>{
      this.id= +params['id'];
      });
      this.edata.getEmployee(this.id).subscribe(
        e => {this.employeeValue = e;
      },
      error => console.log(error)
        
      );
      

      this.edata.getDepartments().subscribe(
        depart => {
          this.department = depart;
        }
      );
    
  }

  editemployee(){
    console.log("inside edit method1"); 
    this.http.put(this.url+`/${this.id}`,JSON.stringify(this.employeeValue),{headers: new HttpHeaders({'Content-Type': 'application/json'})}).subscribe(
      ()=>{
        console.log("inside edit method2");
        return this.edata.getEmployees();
      }
    );
    //this.route.navigate(['/empList']);
  }

}
