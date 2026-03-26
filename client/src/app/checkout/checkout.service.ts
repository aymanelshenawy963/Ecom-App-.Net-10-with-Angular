import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Delivery } from '../shared/Models/Delivery';
import { ICreateOrder, IOrder } from '../shared/Models/Orders';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseURL=environment.baseURL;
  constructor(private http:HttpClient) { }

  getAddress(){
    return this.http.get(this.baseURL+"account/get-address-for-user");
  }
  updateAddress(form:any){
    return this.http.put(this.baseURL+"account/update-address",form);
  }

  getDeliveryMethod(){
    return this.http.get<Delivery[]>(this.baseURL+"orders/get-delivery");
  }

  createOrder(order:ICreateOrder){
    return this.http.post<IOrder>(this.baseURL+"orders/create-order",order);
  }
}
