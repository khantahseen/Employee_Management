import { Component , OnInit} from '@angular/core';

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
    
  }
  
  logout(){
    localStorage.clear();
    console.log("aaaa");
    console.log(localStorage.getItem('userToken'));
   
  }

}
