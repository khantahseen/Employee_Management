import { Component, OnInit } from '@angular/core';
import {department} from '../Models/Department';
import {employee} from '../Models/Employee';
import { NgForm } from "@angular/forms";
import { Router } from '@angular/router';
import { DataService } from '../Shared/data.service';



@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {

  constructor(private employeedata:DataService,private route:Router) { 
  }
  department:department[];
  employeeValue:employee={
    EmployeeId:0,
    Name:null,
    Surname:null,
    Email:null,
    Address:null,
    Qualification:null,
    ContactNumber:0,
    DepartmentID:0
  };

  ngOnInit(): void {
    this.employeedata.getDepartments().subscribe(
      depart => {
        this.department = depart;
      }
    );
  }

  addemployee(){
    this.employeedata.addEmployee(this.employeeValue);
    this.route.navigate(['/empList']);
  }

}
