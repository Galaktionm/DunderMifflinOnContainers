import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerBasket } from './CustomerBasket';
import { Product } from '../Product';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  basketProducts!:Product[]

  constructor(private http:HttpClient){}

  ngOnInit(): void {
      this.getCart();
  }

  getCart(){
    var url="https://localhost:7098/api/basket/"+sessionStorage.getItem("userId");
    this.http.get<CustomerBasket>(url).subscribe({
      next: (result)=>{
        this.basketProducts=result.products;
        console.log(result);
      },
      error: (error)=>{
        console.log(error)
      }
    });

  }

  submitOrder(){
    var url="https://localhost:7098/api/basket/"+sessionStorage.getItem("userId");
    this.http.post(url, this.basketProducts).subscribe({
      next: (result)=>{
        console.log(result);
      },
      error: (error)=>{
        console.log(error)
      }
    })
  }

}
