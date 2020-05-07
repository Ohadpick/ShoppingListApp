import { Component, OnInit, Input } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { AuthService } from 'src/app/_services/auth.service';
import { BasketService } from 'src/app/_services/basket.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css']
})
export class ItemCardComponent implements OnInit {
  @Input() item: Item;

  constructor(private authService: AuthService, private basketService: BasketService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

}
