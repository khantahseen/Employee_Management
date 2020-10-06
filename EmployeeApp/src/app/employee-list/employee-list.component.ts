import { Component, OnInit } from '@angular/core';
import { employee } from '../Models/Employee';
import { DataService } from '../Shared/data.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees:employee[];
  emp:employee={
    EmployeeId:0,
    Name:null,
    Surname:null,
    Email:null,
    Address:null,
    Qualification:null,
    ContactNumber:0,
    DepartmentID:0
  };

  empwithsamedept:employee[];
  errorMessage:string;
  constructor(public employeedata:DataService) {}
  //e1:Array<employee>;
  ngOnInit(): void {
    var empEmail=localStorage.getItem('userName');
    this.employeedata.getEmployees().subscribe(
      emp=>{
        this.employees=emp;
        if(localStorage.getItem('userRole')=='Employee')
        {
          this.emp=this.employees.find(e=>e.Email==empEmail);
          this.empwithsamedept=emp.filter(e=>e.DepartmentID==this.emp.DepartmentID);
        }
        else{
          this.empwithsamedept=emp
        }
      }
      //e1 => {
        //return this.e1 = e1;
      //},
      //error => this.errorMessage = <any>error
    //);
    //console.log(this.e1);
    );
  }

  delete(id:number){
    this.employeedata.deleteEmployee(id);
    window.location.reload();
    
}
}
