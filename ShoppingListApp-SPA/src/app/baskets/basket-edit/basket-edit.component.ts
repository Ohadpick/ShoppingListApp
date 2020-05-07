import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { Basket } from 'src/app/_models/basket';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BasketService } from 'src/app/_services/basket.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-basket-edit',
  templateUrl: './basket-edit.component.html',
  styleUrls: ['./basket-edit.component.css']
})
export class BasketEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  basket: Basket;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue  = true;
    }
  }

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
              private basketService: BasketService, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.basket = data['basket'];
    });
  }

  updateBasket() {
    this.basketService.updateBasket(this.authService.decodedToken.nameid, this.basket).subscribe (next => {
      this.alertify.success('Basket update successfuly');
      this.editForm.reset(this.basket);
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteBasketItem(id: number) {
    this.alertify.confirm('Are you sure you want to delete this photo?', () =>  {
      this.basketService.deleteBasketItem(this.authService.decodedToken.nameid, this.basket.id, id).subscribe(() => {
        this.basket.basketItems.splice(this.basket.basketItems.findIndex (i => i.id === id), 1);
        this.alertify.success('Item has been deleted successfully');
      }, error => {
          this.alertify.error('Failed to delete item');
        });
    });
  }
}
