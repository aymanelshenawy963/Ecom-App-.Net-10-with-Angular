import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { IOType } from 'child_process';
import { IOrder } from '../shared/Models/Orders';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  baseURL = environment.baseURL;
  constructor(private http:HttpClient) { }

  getCurrentOrderForUser(id:number){
   return this.http.get<IOrder>(this.baseURL+"orders/get-order-by-id/"+id);
  }

  getAllOrdersForUser(){
   return this.http.get<IOrder[]>(this.baseURL+"orders/get-orders-for-user/");
  }

}
