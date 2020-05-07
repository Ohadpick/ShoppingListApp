import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BasketService } from '../_services/basket.service';
import { Item } from '../_models/item';

@Injectable ()
export class ItemListResolver implements Resolve<Item[]> {
    pageNumber = 1;
    pageSize = 4;

    constructor(private basketService: BasketService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Item[]> {
        return this.basketService.getBaskets(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}