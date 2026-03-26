import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { IProduct } from '../../shared/Models/Product';
import { ActivatedRoute } from '@angular/router';
import { __values } from 'tslib';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {

  constructor(private shopService:ShopService,
    private route:ActivatedRoute,
    private toast:ToastrService,
    private basketService:BasketService
  ){}

  quantity:number=1;
  product:IProduct
  ngOnInit(): void {
    this.loadProduct()
  }

  mainImage:string;
  loadProduct(){
    this.shopService.getProductDetails(parseInt(this.route.snapshot.paramMap.get('id')))
    .subscribe({
      next:((value:IProduct)=>{
        this.product=value;
        this.mainImage=this.product.photos[0].imageName
      })
    })
  }

  ReplaceImage(src:string){
    this.mainImage=src
  }

  IncrementBasket(){
    if(this.quantity<10){
    this.quantity++;
    this.toast.success("item has been added to the basket","SUCCESS")
    }else{
      this.toast.warning("you can't add more than 10 items","Enough")
    }
  }


  DecrementBasket(){
    if(this.quantity>1){
    this.quantity--;
    this.toast.success("item has been Decrement","SUCCESS")
    }else{
      this.toast.error("you can't Decrement more than 1 items","ERROR")
    }
  }

  AddItemToBasket(){
    this.basketService.AddItemToBasket(this.product,this.quantity)
  }

  CalucateDiscount(oldPrice:number,newPrice:number):number{
    return parseFloat(
      Math.round(((oldPrice-newPrice)/oldPrice)*100).toFixed(1)
    )
  }
}


