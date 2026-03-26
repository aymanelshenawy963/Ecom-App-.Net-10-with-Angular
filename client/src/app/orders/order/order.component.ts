import { Component, OnInit } from '@angular/core';
import { IOrder, IOrderItem } from '../../shared/Models/Orders';
import { OrdersService } from '../orders.service';
declare var bootstrap:any;

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent implements OnInit {
  orders:IOrder[]=[];
  URLImageModal:string[]=[]

  constructor(private _service:OrdersService){}

  ngOnInit(): void {
    this._service.getAllOrdersForUser().subscribe({
      next:response=>{
        this.orders=response;
        console.log(this.orders)
      },
      error:err=>{
        console.log(err)
      }
    })
  }

  OpenModal(orderItems:IOrderItem[]){
    this.URLImageModal=orderItems.map(item=>item.mainImage);
    var modal = document.getElementById('ImageModal');
    var modalElement = new bootstrap.Modal(modal);
    modalElement.show();
  }
  CloseModal(){
    var modal = document.getElementById('ImageModal');
    var instance = new bootstrap.Modal.getInstance(modal);
    instance.hide();
  }

  getFirstImageOrderItem(order:IOrderItem[]){
    return order.length>0 ? order[0].mainImage:null;

  }

}
