import { Component, OnInit } from '@angular/core';
import { IBasketTotal } from '../../Models/Basket';
import { BasketService } from '../../../basket/basket.service';

@Component({
  selector: 'app-order-total',
  templateUrl: './order-total.component.html',
  styleUrl: './order-total.component.scss'
})
export class OrderTotalComponent implements OnInit {
  basketTotal:IBasketTotal;
  constructor(private basketservice:BasketService){}
  ngOnInit(): void {
    this.basketservice.basketTotal$.subscribe({
       next:(value)=>{
        this.basketTotal=value
       },error(err){
        console.log(err)
       }
    })
  }

}
