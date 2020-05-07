import { Component, OnInit } from '@angular/core';
import { Basket } from 'src/app/_models/basket';
import { Pagination, PaginationResult } from 'src/app/_models/pagination';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/_services/basket.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-basket-list',
  templateUrl: './basket-list.component.html',
  styleUrls: ['./basket-list.component.css']
})
export class BasketListComponent implements OnInit {
  baskets: Basket[];
  generalParams: any = {};
  pagination: Pagination;

  constructor(private basketService: BasketService, private alertify: AlertifyService,
              private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.baskets = data['baskets'].result;
      this.pagination = data['baskets'].pagination;
    });

    this.resetFilters();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadBaskets();
  }

  resetFilters() {
    this.generalParams.orderBy = 'description';
    this.loadBaskets();
  }

  loadBaskets() {
    this.basketService.getBaskets(this.authService.decodedToken.nameid,
                    this.pagination.currentPage, this.pagination.itemsPerPage, this.generalParams)
        .subscribe((res: PaginationResult<Basket[]>) => {
          this.baskets = res.result;
          this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }
}
