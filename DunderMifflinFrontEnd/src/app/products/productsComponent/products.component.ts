import { Component, OnInit } from '@angular/core';
import { Product } from '../Product';
import { HttpClient } from '@angular/common/http';
import { CustomerBasket } from '../cart/CustomerBasket';
import { CustomerBasketRequest } from './CustomerBasketRequest';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products!:Product[]
  searchedProducts!:Product[]
  searchForm!:FormGroup

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
      this.fetchData();
      this.searchForm=new FormGroup({
        query:new FormControl('')
      });
  }

  fetchData(){
    var url="https://localhost:7152/api/products/all";
    this.http.get<Product[]>(url).subscribe({
      next: (result)=>{
        console.log(result),
        this.products=result
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }

  addToCart(product: Product){
    var url="https://localhost:7098/api/basket";
    var productRequest=new Product(product.id, product.name, product.price, product.manufacturer, product.additionalInfo);
    var userId=sessionStorage.getItem("userId");
    if(userId!=null){
    try{
    var basket=new CustomerBasketRequest(userId, productRequest);
    console.log(basket);
    this.http.post<string>(url, basket).subscribe({
      next: (result)=>{
        console.log(result);
      },
      error: (error)=>{
        console.log(error);
      }
    })
    }catch (error){
      console.log(error);
    }
    }
  }

  searchProduct(){
    var query=this.searchForm.controls["query"].value;
    var request=new ProductsSearchRequest(query);
    var url="https://localhost:7152/api/products";
    this.http.post<Product[]>(url, request).subscribe({
      next: (result)=>{
        this.searchedProducts=result;
        console.log(result);
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }
}

export class ProductsSearchRequest {
  query:string
  constructor(query:string){
    this.query=query;
  }
}
