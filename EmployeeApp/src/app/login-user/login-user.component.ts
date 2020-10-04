import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Router } from '@angular/router';
import {login} from '../Models/Login';
import { DataService } from '../Shared/data.service';

@Component({
  selector: 'app-login-user',
  templateUrl: './login-user.component.html',
  styleUrls: ['./login-user.component.css']
})
export class LoginUserComponent implements OnInit {

  constructor(private logindata:DataService,private route:Router) { 
  }
 
  loginValue:login={
    Email:null,
    Password:null
  };

  ngOnInit(): void {
    
      }
   

  logIn(){
    this.logindata.loginUser(this.loginValue);
    this.route.navigate(['/empList']);
  }

}


