import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { HomeModule } from './home/home.module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { loaderInterceptor } from './core/Interceptor/loader.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { credentialsInterceptor } from './core/Interceptor/credentials.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    HomeModule,
    NgxSpinnerModule,
    ToastrModule.forRoot({
      closeButton:true,
      positionClass:'toast-top-right',
      countDuplicates:true,
      timeOut:1500,
      progressBar:true
    })
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient( withInterceptors([loaderInterceptor])),
    provideHttpClient( withInterceptors([credentialsInterceptor])),
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
