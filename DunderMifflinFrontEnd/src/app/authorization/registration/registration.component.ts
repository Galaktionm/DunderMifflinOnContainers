import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { RegisterRequest } from './RegisterRequest';
import { RegisterResult } from './RegisterResult';
import { AuthService } from '../AuthService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registrationForm!: FormGroup

  constructor(private service:AuthService, private router:Router){}

  ngOnInit(): void {
      this.registrationForm=new FormGroup({
        username: new FormControl(""),
        email: new FormControl(""),
        password: new FormControl("")
      })
  }

  submitRegistration(){
    var username=this.registrationForm.controls["username"].value;
    var email=this.registrationForm.controls["email"].value;
    var password=this.registrationForm.controls["password"].value;

    var registerRequest=new RegisterRequest(username, email, password);

    this.service.register(registerRequest).subscribe({
      next: (result)=>{
        console.log(result);
        this.router.navigate(["/login"])
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }
}
