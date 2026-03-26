import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild, viewChild } from '@angular/core';
import { IPagniation } from '../shared/Models/Pagination';
import { IProduct } from '../shared/Models/Product';
import { ShopService } from './shop.service';
import { ICategory } from '../shared/Models/Category';
import { ProductParam } from '../shared/Models/ProductParam';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{

  constructor(private shopService:ShopService,private toastr:ToastrService){}
  ngOnInit(): void {
    this.getAllProducts();
    this.getAllCategoriess();
  }

  product:IProduct[];
  category:ICategory[];
  totalCount:number;
  productParam = new ProductParam();


  getAllProducts(){
    this.shopService.getProduct(this.productParam).subscribe({
      next:((value:IPagniation)=>{
        this.product=value.data
        this.totalCount=value.totalCount
        this.productParam.pageNumber=value.pageNumber
        this.productParam.pageSize=value.pageSize
        this.toastr.success('Product loaded successfuly',"SUCCESS")
      })
    })
  }
  OnChangePage(event:any){
    this.productParam.pageNumber=event
    this.getAllProducts()
  }
  getAllCategoriess(){
    this.shopService.getCategory().subscribe({
      next:((value:ICategory[])=>{
        this.category=value
      })
    })
  }

  SelectedId(categoryId:number){
    this.productParam.categoryId=categoryId
    this.getAllProducts();
  }

  SortingOptions=[
    {name:'Price',value:'Name'},
    {name:'Price:min-max',value:'priceAsc'},
    {name:'Price:max-min',value:'priceDsc'}
  ]

  sortingByPrice(sort:Event){
    this.productParam.sortSelected=(sort.target as HTMLInputElement).value;
    this.getAllProducts()
  }

  OnSearch(Search:string){
      this.productParam.search=Search;
      this.getAllProducts()
  }

  @ViewChild('search') searchInput: ElementRef;
  @ViewChild('sortSelected') selected : ElementRef;

  ResetValue(){
    this.productParam.categoryId=0;
    this.productParam.sortSelected= this.SortingOptions[0].value;
    this.productParam.search='';

    this.searchInput.nativeElement.value='';

    this.selected.nativeElement.selectedIndex=0;

    this.getAllProducts()
  }
}
