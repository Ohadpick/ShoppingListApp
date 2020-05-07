import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginationResult } from '../_models/pagination';
import { Basket } from '../_models/basket';
import { map } from 'rxjs/operators';
import { Item } from '../_models/item';
import { Category } from '../_models/category';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getBaskets(userId: number, page?, itemsPerPage?, generalParam?): Observable<PaginationResult<Basket[]>> {
    const paginatedResult: PaginationResult<Basket[]> = new PaginationResult<Basket[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (generalParam) {
      params = params.append('orderBy', generalParam.orderBy);
    }

    return this.http.get<Basket[]>(this.baseUrl + 'users/' + userId + '/baskets', { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getBasket(userId: number, id: number): Observable<Basket> {
    return this.http.get<Basket>(this.baseUrl + 'users/' + userId + '/baskets/' + id);
  }

  updateBasket(userId: number, basket: Basket) {
    return this.http.put (this.baseUrl + 'users/' + userId + '/baskets/' + basket.id, basket);
  }

  deleteBasketItem(userId: number, basketId: number, id: number) {
    return this.http.delete (this.baseUrl + 'users/' + userId + '/baskets/' + basketId + '/basketItems/' + id);
  }

  getItems(userId: number, page?, itemsPerPage?, generalParam?): Observable<PaginationResult<Item[]>> {
    const paginatedResult: PaginationResult<Item[]> = new PaginationResult<Item[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (generalParam.orderBy) {
      params = params.append('orderBy', generalParam.orderBy);
    }

    if (generalParam.category) {
      params = params.append('category', generalParam.category);
    }

    return this.http.get<Item[]>(this.baseUrl + 'users/' + userId + '/items', { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getItem(userId: number, id: number): Observable<Item> {
    return this.http.get<Item>(this.baseUrl + 'users/' + userId + '/items/' + id);
  }

  getCategories(userId: number, page?, itemsPerPage?, generalParam?): Observable<PaginationResult<Category[]>> {
    const paginatedResult: PaginationResult<Category[]> = new PaginationResult<Category[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (generalParam) {
      params = params.append('orderBy', generalParam.orderBy);
    }

    return this.http.get<Category[]>(this.baseUrl + 'users/' + userId + '/categories', { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getCategory(userId: number, id: number): Observable<Category> {
    return this.http.get<Category>(this.baseUrl + 'users/' + userId + '/categories/' + id);
  }
}
