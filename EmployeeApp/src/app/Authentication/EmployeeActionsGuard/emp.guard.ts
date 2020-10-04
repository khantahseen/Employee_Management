import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree , Router} from '@angular/router';
import { Observable } from 'rxjs';
import {DataService} from 'src/app/Shared/data.service';

@Injectable({
  providedIn: 'root'
})
export class EmpGuard implements CanActivate {
  constructor(private router : Router, private dataService:DataService){}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot):  boolean {
      if(this.dataService.checkRoleEmployee())
      {
        this.router.navigate(['/noAccess']);
        return false;}
      else{
        
        return true;
      }
    }
  }

