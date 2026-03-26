import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BasketRoutingModule } from './basket-routing.module';
import { BasketComponent } from './basket/basket.component';
import { SharedModule } from '../shared/shared.module';
import { A11yModule } from "@angular/cdk/a11y";


@NgModule({
  declarations: [
    BasketComponent
  ],
  imports: [
    CommonModule,
    BasketRoutingModule,
    SharedModule,
    A11yModule,
    
]
})
export class BasketModule { }
