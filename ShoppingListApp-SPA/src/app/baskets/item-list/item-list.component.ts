import { Component, OnInit, Input } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { Pagination, PaginationResult } from 'src/app/_models/pagination';
import { BasketService } from 'src/app/_services/basket.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/_models/category';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {
  items: Item[];
  categories: Category[];
  generalParams: any = {};
  pagination: Pagination;
  selectedCategory = 1;

  constructor(private basketService: BasketService, private alertify: AlertifyService,
              private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.items = data['items'].result;
      this.pagination = data['items'].pagination;

      this.resetFilters();
      this.loadCategories();
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadItems();
  }

  resetFilters() {
    this.generalParams.orderBy = 'description';
    // if (this.categories != null) {
    //   this.generalParams.category = this.categories[0];
    // }
    this.loadItems();
  }

  loadItems() {
    this.generalParams.category = this.selectedCategory;
    this.basketService.getItems(this.authService.decodedToken.nameid,
                  this.pagination.currentPage, this.pagination.itemsPerPage, this.generalParams)
        .subscribe((res: PaginationResult<Item[]>) => {
          this.items = res.result;
          this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  loadCategories() {
    this.basketService.getCategories(this.authService.decodedToken.nameid,
                  this.pagination.currentPage, this.pagination.itemsPerPage, this.generalParams)
        .subscribe((res: PaginationResult<Category[]>) => {
          this.categories = res.result;
    }, error => {
      this.alertify.error(error);
    });
  }

  changeCategory(category: number) {
    this.selectedCategory = category;
    this.loadItems();
  }

}
