import { Component, OnInit, Input } from '@angular/core';
import { Basket } from 'src/app/_models/basket';
import { AuthService } from 'src/app/_services/auth.service';
import { BasketService } from 'src/app/_services/basket.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-basket-card',
  templateUrl: './basket-card.component.html',
  styleUrls: ['./basket-card.component.css']
})
export class BasketCardComponent implements OnInit {
  @Input() basket: Basket;
  numOfBasketItemsToDisplay = 3;
  
  constructor(private authService: AuthService, private basketService: BasketService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

}
