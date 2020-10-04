import { Component, OnInit } from '@angular/core';
import { department } from '../Models/Department';
import { DataService } from '../Shared/data.service';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements OnInit {

  errorMessage:string;
  
  constructor(private departmentdata:DataService) {}
  d1:Array<department>;
  ngOnInit(): void {
    this.departmentdata.getDepartments().subscribe(
      d1 => {
        return this.d1 = d1;
      },
      error => this.errorMessage = <any>error
    );
    console.log(this.d1);
  }

  delete(id:number){
    this.departmentdata.deleteDepartment(id);
    window.location.reload();
    
}

}
