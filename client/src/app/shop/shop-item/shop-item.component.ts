import { Component, Input, input } from '@angular/core';
import { IProduct } from '../../shared/Models/Product';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-shop-item',
  templateUrl: './shop-item.component.html',
  styleUrl: './shop-item.component.scss'
})
export class ShopItemComponent {
  constructor(private _service:BasketService){}
 @Input() Product:IProduct;

 SetBasketValue(){
  this._service.AddItemToBasket(this.Product)
 }
}

