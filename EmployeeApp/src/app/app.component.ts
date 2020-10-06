import { Component , OnInit} from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'EmployeeApp';
  name=localStorage.getItem('userName');
  constructor() { }

  ngOnInit(): void {
    var connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:44318/NotificationHub",{accessTokenFactory:()=>localStorage.getItem('userToken')})
      .build();
    //connection.start().then(function() {
     // console.log('connected');
      connection.on('departmentAdded',function(message){
        alert(message);
        console.log(message);
      });
    

    //connection.start().then(function() {
      //console.log('connected');
      connection.on('employeeAdded',function(message){
        alert(message);
        console.log(message);
      });

      connection.on('employeeEdit',function(message){
        alert(message);
        console.log(message);
      });
    
      try {
        connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
    }
  }
  
  logout(){
    localStorage.clear();
    console.log("aaaa");
    console.log(localStorage.getItem('userToken'));
   }
}
