import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AccountComponent } from './account/account.component';



@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent,
    AccountComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule
  ], exports: [
    LoginComponent,
    RegistrationComponent,
    AccountComponent
  ]
})
export class AuthorizationModule { }
