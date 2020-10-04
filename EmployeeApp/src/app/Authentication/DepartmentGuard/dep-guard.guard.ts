import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import {DataService} from 'src/app/Shared/data.service';

@Injectable({
  providedIn: 'root'
})
export class DepGuardGuard implements CanActivate {
  constructor(private router : Router, private dataService:DataService){}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot):  boolean {
      if(this.dataService.checkRoleAdmin())
      {return true;}
      else{
        this.router.navigate(['/noAccess']);
        return false;
      }
    }
  }
