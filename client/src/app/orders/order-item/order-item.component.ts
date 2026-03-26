import { Component, OnInit } from '@angular/core';
import { IOrder } from '../../shared/Models/Orders';
import { ActivatedRoute } from '@angular/router';
import { OrdersService } from '../orders.service';
import { response } from 'express';
import { error } from 'console';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrl: './order-item.component.scss'
})
export class OrderItemComponent implements OnInit {
  order: IOrder;
  id: number=0;

  constructor(private route: ActivatedRoute, private _service: OrdersService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(param => {
      this.id = param['id']
    });

    this._service.getCurrentOrderForUser(this.id).subscribe({
      next: response => {
        this.order = response;
        console.log(this.order)
      },error: err => {
        console.log(err)
      }
    })
  }

}
