import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from '../../basket/basket.service';
import { IBasket } from '../../shared/Models/Basket';
import { ICreateOrder } from '../../shared/Models/Orders';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.scss'
})
export class PaymentComponent implements OnInit {

  @Input() delivery: FormGroup;
  @Input() address: FormGroup;

  constructor(private _service: CheckoutService, private toast: ToastrService,
    private _basketService: BasketService,private router:Router) { }

  ngOnInit(): void {
  }

  CreateOrder() {
    const basket = this._basketService.GetCurrentValue();
    const order = this.GetOrder(basket);
    this._service.createOrder(order).subscribe({
      next: (value) => {
        this._basketService.clearLocalBasket();
        this.router.navigate(['/checkout/success'],{queryParams:{orderId:value.id}});
        this.toast.success("Order Created Successfly", "SUCCESS")
      },
      error: (err) => {
        console.log(err);
        this.toast.error("Something went wrong");
      }
    })
  }
  GetOrder(basket: IBasket): ICreateOrder {
    return {
      basketId: basket.id,
      deliveyMethodId: this.delivery.value.delivery,
      shippingAddress: this.address.value
    }

  }

}
