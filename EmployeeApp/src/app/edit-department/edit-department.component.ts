import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { department } from '../Models/Department';
import { employee } from '../Models/Employee';
import { DataService } from '../Shared/data.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-edit-department',
  templateUrl: './edit-department.component.html',
  styleUrls: ['./edit-department.component.css']
})
export class EditDepartmentComponent implements OnInit {

  constructor(private ddata:DataService,private route:Router,private ar:ActivatedRoute, private http:HttpClient) { 
  }
  id:number=0;
  url:string='https://localhost:44318/api/DepartmentsApi';
  departmentValue:department = {
    DepartmentID: 0,
    Name: null,
  };

  ngOnInit(): void {
    this.ar.params.subscribe(params =>{
      this.id= +params['id'];
      });
      this.ddata.getDepartment(this.id).subscribe(
        e => {this.departmentValue = e;
      },
      error => console.log(error)
      );
  }

  editdepartment(){
    console.log("inside edit method1"); 
    this.http.put(this.url+`/${this.id}`,JSON.stringify(this.departmentValue),{headers: new HttpHeaders({'Content-Type': 'application/json'})}).subscribe(
      ()=>{
        console.log("inside edit method2");
        return this.ddata.getEmployees();
      }
    );
    this.route.navigate(['/deptList']);
  }

}
