import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/Models/Basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent implements OnInit {

  count$: Observable<IBasket>;

  constructor(private basketService: BasketService){}

  ngOnInit(): void {
      const basketId = localStorage.getItem('basketId');
      
        this.basketService.GetBasket(basketId).subscribe({
          next(value){
            console.log(value)
          },
          error:(err)=>{
            console.log(err)
          }
        })

      this.count$ = this.basketService.basket$;
      }



  visible:boolean = false

  ToggelDropDown(){
    this.visible=!this.visible
  }
}
