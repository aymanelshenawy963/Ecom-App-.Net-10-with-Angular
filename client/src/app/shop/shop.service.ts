import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagniation } from '../shared/Models/Pagination';
import { ICategory } from '../shared/Models/Category';
import { ProductParam } from '../shared/Models/ProductParam';
import { IProduct } from '../shared/Models/Product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseURL='https://localhost:44361/api/';

  constructor(private http:HttpClient){}

  getProduct(producttParam:ProductParam){
    let param = new HttpParams();
    if(producttParam.categoryId){
      param = param.append("categoryId",producttParam.categoryId)
    }

    if(producttParam.sortSelected){
      param = param.append("sort",producttParam.sortSelected)
    }

    if(producttParam.search){
      param = param.append("Search",producttParam.search)
    }
      param = param.append("pageNumber",producttParam.pageNumber)
      param = param.append("pageSize",producttParam.pageSize)

    return this.http.get<IPagniation>(this.baseURL+"Products/get-all",{params:param});
  }

  getProductDetails(id:number){
    return this.http.get<IProduct>(this.baseURL+"Products/get-by-id/"+id)
  }
    getCategory(){
    return this.http.get<ICategory[]>(this.baseURL+"Categories/get-all");
  }


}
