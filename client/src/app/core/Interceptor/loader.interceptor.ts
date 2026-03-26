import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs';
import { LoadingService } from '../Services/loading.service';

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {

  const loadingService = inject(LoadingService);

  // تجاهل طلبات الباسكت
  if(req.url.includes('basket')){
    return next(req);
  }

  // تشغيل اللودنج لباقي الطلبات
  loadingService.loading();

  return next(req).pipe(
    // delay(1000),
    finalize(() => {
      loadingService.hideLoader();
    })
  );

};
