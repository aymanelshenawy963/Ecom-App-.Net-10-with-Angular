import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Delivery } from '../../shared/Models/Delivery';
import { CheckoutService } from '../checkout.service';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrl: './delivery.component.scss'
})
export class DeliveryComponent implements OnInit {

  @Input() delivery: FormGroup;

  deliveries: Delivery[] = [];

  constructor(private _service: CheckoutService, private _basketService:BasketService) { }

  SetShippingPrice(){
    const delivery = this.deliveries.find(m=>m.id==this.delivery.value.delivery);
    this._basketService.setShippingPrice(delivery)
  }
  ngOnInit(): void {
    this._service.getDeliveryMethod().subscribe({
      next: (value) => {
        this.deliveries=value
      },
      error(err) {
        console.log(err)
      }
    })
  }

}
