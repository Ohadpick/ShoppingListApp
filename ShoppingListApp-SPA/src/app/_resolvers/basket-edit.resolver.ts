import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { Basket } from '../_models/basket';
import { BasketService } from '../_services/basket.service';

@Injectable ()
export class BasketEditResolver implements Resolve<Basket> {
    constructor(private basketService: BasketService, private router: Router, private alertify: AlertifyService,
                private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Basket> {
        return this.basketService.getBasket(this.authService.decodedToken.nameid, route.params['id'] ).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving your data');
                this.router.navigate(['/baskets']);
                return of(null);
            })
        );
    }
}