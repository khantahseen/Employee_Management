import { Component, OnInit } from '@angular/core';
import {department} from '../Models/Department';
import {employee} from '../Models/Employee';
import { NgForm } from "@angular/forms";
import { Router } from '@angular/router';
import { DataService } from '../Shared/data.service';


@Component({
  selector: 'app-add-department',
  templateUrl: './add-department.component.html',
  styleUrls: ['./add-department.component.css']
})
export class AddDepartmentComponent implements OnInit {

  departmentValue:department = {
    DepartmentID: 0,
    Name: null,
  };
  constructor(private departmentdata:DataService,private route:Router) { }
  
  ngOnInit(): void {
  }

  adddepartment(){
    this.departmentdata.addDepartment(this.departmentValue);
    this.route.navigate(['/deptList']);
  }

}
