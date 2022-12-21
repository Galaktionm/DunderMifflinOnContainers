import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from './login/LoginRequest';
import { LoginResult } from './login/LoginResult';
import { RegisterRequest } from './registration/RegisterRequest';
import { RegisterResult } from './registration/RegisterResult';


@Injectable({
 providedIn: 'root',
})
export class AuthService {
 constructor(protected http: HttpClient) {}

 getToken():string|null{
    var token=sessionStorage.getItem("token");
   return token; 
 }

 login(item: LoginRequest): Observable<LoginResult> {
    var url = "https://localhost:7015/" + "api/authorization/login";
    return this.http.post<LoginResult>(url, item);
}

logout() {
    localStorage.removeItem("token");
}

register(item: RegisterRequest):Observable<RegisterResult>{
    var url = "https://localhost:7015/" + "api/authorization/register";
    return this.http.post<RegisterResult>(url, item);
}

isAuthenticated():boolean{
    if(sessionStorage.getItem("userId")!=null){
        return true;
    }
    return false;
}

}