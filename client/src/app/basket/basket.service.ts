import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Basket, IBasket, IBasketItem, IBasketTotal } from '../shared/Models/Basket';
import { IProduct } from '../shared/Models/Product';
import { Delivery } from '../shared/Models/Delivery';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private http:HttpClient) { }

  baseURL='https://localhost:44361/api/';
  private basketSource = new BehaviorSubject<IBasket | null>(null);
  basket$ = this.basketSource.asObservable();
  private basketSourceTotal = new BehaviorSubject<IBasketTotal | null>(null);
  basketTotal$= this.basketSourceTotal.asObservable();
  shipPrice:number=0;

  setShippingPrice(delivery:Delivery){
    this.shipPrice=delivery.price;
    this.claculateTotal();
  }

  claculateTotal(){
    const basket = this.GetCurrentValue();
    const shipping = this.shipPrice;
    const subTotal = basket.basketItems.reduce((a,c)=>{
     return (c.price*c.quantity)+a;
    },0);
    const total = shipping+subTotal;
    this.basketSourceTotal.next({shipping,subTotal,total});
  }

GetBasket(id:string){
  return this.http.get<IBasket>(this.baseURL+"baskets/get-basket/"+id).pipe(
    map((value:IBasket)=>{
      this.basketSource.next(value);
      this.claculateTotal()
      return value
    })
  )
}

  SetBasket(basket:IBasket){
    return this.http.post(this.baseURL+"baskets/update-basket/",basket).subscribe({
      next:(value:IBasket) => {
         this.basketSource.next(value);
         this.claculateTotal();
         console.log(value)
      },
      error(err){
      console.log(err)
    },
    })
  }

  GetCurrentValue(){
   return this.basketSource.value;
  }

AddItemToBasket(product:IProduct,quantity:number=1){

  const itemToAdd = this.MapProductToBasketItem(product,quantity);

  let basket = this.GetCurrentValue();

if (!basket || basket.id == null) {
  basket = this.CreateBasket();
}

  basket.basketItems = this.AddOrUpdate(basket.basketItems,itemToAdd,quantity);

  return this.SetBasket(basket);
}

  AddOrUpdate(basketItems: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = basketItems.findIndex(i=>i.id===itemToAdd.id);
    if(index==-1){
      itemToAdd.quantity=quantity;
      basketItems.push(itemToAdd);
    }else{
      basketItems[index].quantity += quantity
    }
    return basketItems;
  }



  private CreateBasket(): IBasket {

  const basket = new Basket();

  if (typeof window !== 'undefined') {
    localStorage.setItem('basketId', basket.id);
  }

  return basket;
}

  private MapProductToBasketItem(product: IProduct, quantity: number): IBasketItem {
    return{
      id:product.id,
      productName:product.name,
      description:product.description,
      image:product.photos[0].imageName,
      price:product.newPrice,
      quantity:quantity,
      category:product.categoryName
    }
  }

  IncrementBasketItemQuantity(item:IBasketItem){
    const basket = this.GetCurrentValue();
    const itemIndex = basket.basketItems.findIndex(i=>i.id===item.id);
    basket.basketItems[itemIndex].quantity++;
    this.SetBasket(basket);
  }

    DecrementBasketItemQuantity(item:IBasketItem){
    const basket = this.GetCurrentValue();
    const itemIndex = basket.basketItems.findIndex(i=>i.id===item.id);
    if(basket.basketItems[itemIndex].quantity>1){
    basket.basketItems[itemIndex].quantity--;
    this.SetBasket(basket);
    }else{
      this.RemoveItemFromBasket(item)
    }
  }
  RemoveItemFromBasket(item:IBasketItem) {
   const basket = this.GetCurrentValue();
   if(basket.basketItems.some(i=>i.id===item.id)){
    basket.basketItems = basket.basketItems.filter(i=>i.id !== item.id)
   }
   if(basket.basketItems.length>0){
    this.SetBasket(basket)
   }else{
    this.DeleteBasketItem(basket);
   }
  }
  DeleteBasketItem(basket:IBasket) {
    return this.http.delete(this.baseURL+"baskets/delete-basket/"+basket.id)
    .subscribe({
      next:(value)=>{
        this.basketSource.next(null);
        localStorage.removeItem('basketId')
      },error(err){
        console.log(err)
      }
    })
  }

  clearLocalBasket() {
  this.basketSource.next(null);
  this.basketSourceTotal.next(null);
  localStorage.removeItem('basketId');
}
}
