
import { department } from './Department';


export interface employee{
    EmployeeId:number,
    Name:string,
    Surname:string,
    Email:string,
    Address:string,
    Qualification:string,
    ContactNumber:number,
    DepartmentID:number,
    department?: department,
}