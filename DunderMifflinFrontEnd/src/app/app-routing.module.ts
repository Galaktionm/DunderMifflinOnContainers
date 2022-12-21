import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './authorization/account/account.component';
import { LoginComponent } from './authorization/login/login.component';
import { RegistrationComponent } from './authorization/registration/registration.component';
import { CartComponent } from './products/cart/cart.component';
import { ProductsComponent } from './products/productsComponent/products.component';

const routes: Routes = [
  {path: "register", component: RegistrationComponent},
  {path: "login", component: LoginComponent},
  {path:"products", component: ProductsComponent},
  {path:"account", component: AccountComponent},
  {path:"cart", component: CartComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
