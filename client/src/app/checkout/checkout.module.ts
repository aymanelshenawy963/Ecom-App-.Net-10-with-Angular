import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CheckoutRoutingModule } from './checkout-routing.module';
import { CheckoutComponent } from './checkout/checkout.component';
import { StepperComponent } from './stepper/stepper.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatStepperModule} from '@angular/material/stepper';
import {MatButtonModule} from '@angular/material/button';
import { SharedModule } from "../shared/shared.module";
import { AddressComponent } from './address/address.component';
import { DeliveryComponent } from './delivery/delivery.component';
import {MatRadioModule} from '@angular/material/radio';
import { PaymentComponent } from './payment/payment.component';
import { SuccessComponent } from './success/success.component';

@NgModule({
  declarations: [
    CheckoutComponent,
    StepperComponent,
    AddressComponent,
    DeliveryComponent,
    PaymentComponent,
    SuccessComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    MatButtonModule,
    MatStepperModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MatRadioModule
],
  exports:[
    StepperComponent,
    AddressComponent,
    DeliveryComponent,
    PaymentComponent
  ]
})
export class CheckoutModule { }
