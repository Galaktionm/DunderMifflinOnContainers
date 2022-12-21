import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/authorization/AuthService';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isAuthenticated!:boolean

  constructor(private service:AuthService){}

  ngOnInit(): void {
    this.getStatus();
  }

  getStatus(){
    this.isAuthenticated=this.service.isAuthenticated();
  }

  







}
