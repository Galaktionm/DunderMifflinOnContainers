import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../AuthService';
import { LoginRequest } from './LoginRequest';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup

  constructor(private service:AuthService, private router:Router){}

ngOnInit(): void {
    this.loginForm=new FormGroup({
      email: new FormControl(""),
      password: new FormControl("")
    })
}

submitLogin(){
  var email=this.loginForm.controls['email'].value;
  var password=this.loginForm.controls['password'].value;

  var loginRequest=new LoginRequest(email, password);

  this.service.login(loginRequest).subscribe({
    next: (result)=>{
      console.log(result);
      sessionStorage.setItem("userId", result.userId);
      sessionStorage.setItem("token", result.token);
      this.router.navigate(["/"]).then(()=>window.location.reload())
    },
    error: (error)=>{
      console.log(error)
    }
  })
}



}
