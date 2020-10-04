import { HttpInterceptor, HttpRequest, HttpHandler, HttpUserEvent, HttpEvent } from "@angular/common/http";
//import { Observable } from "rxjs/Observable";
import { Observable, throwError } from 'rxjs';
//import { DataService } from 'src/app/Shared/data.service';
//import 'rxjs/add/operator/do';
import { tap } from 'rxjs/operators';
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable()
export class DataInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        //if (req.headers.get('No-Auth') == "True")
            //return next.handle(req.clone());


        request= request.clone({
          setHeaders:{
            'Content-Type':'application/json; charset=utf-8',
            'Accept' :'application/json',
            'Authorization': 'Bearer '+ localStorage.getItem('userToken')
          },
        });

        return next.handle(request);}}

        //if (localStorage.getItem('userToken') != null) {
          //  const clonedreq = req.clone({
            //    headers: req.headers.set('Authorization', 'Bearer '+ localStorage.getItem('userToken'))
            //});
            //return next.handle(clonedreq).pipe(
              //  tap(
                //  succ => {},
                 // err => {
                   // if (err.status === 401) {
                     // this.router.navigateByUrl('/loginForm');
                    //} else if ((err.status = 403)) {
                      //this.router.navigateByUrl('/loginForm');
                      // alert(err.localStorage.getItem('userToken'));
                    //}
                  //}
                //)
              //);
        //}
        //else {
          //  this.router.navigateByUrl('/loginForm');
        //}
    //}
//}