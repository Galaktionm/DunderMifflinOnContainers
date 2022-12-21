import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './User';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  user!: User

  constructor(private http: HttpClient){}

  ngOnInit(): void {
      this.getUser();
  }

  getUser(){
    var url="https://localhost:7015/api/authorization/account"
    var request=new UserRequest(sessionStorage.getItem("userId")!);
    console.log(sessionStorage.getItem("userId"));
    this.http.post<User>(url, request).subscribe({
      next: (result)=>{
        this.user=result;
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }
}

export class UserRequest{
  userId:string

  constructor(userId:string){
    this.userId=userId;
  }
}
