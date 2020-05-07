import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Basket } from '../_models/basket';
import { BasketService } from '../_services/basket.service';

@Injectable ()
export class BasketListResolver implements Resolve<Basket[]> {
    pageNumber = 1;
    pageSize = 4;

    constructor(private basketService: BasketService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Basket[]> {
        return this.basketService.getBaskets(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}