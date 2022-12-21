import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './authorization/registration/registration.component';
import { LoginComponent } from './authorization/login/login.component';
import { AuthService } from './authorization/AuthService';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthorizationModule } from './authorization/authorization.module';
import { ProductsModule } from './products/products.module';
import { AuthInterceptor } from './authorization/AuthInterceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CoreModule } from './core/core.module';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    AuthorizationModule,
    ProductsModule,
    CoreModule
  ],
  providers: [AuthService,
    { provide: HTTP_INTERCEPTORS, 
      useClass: AuthInterceptor, 
      multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
