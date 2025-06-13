import { Component } from '@angular/core';
import { UserDataService } from '../services/user-data-service';
import { UserModel } from '../models/usermodel';

@Component({
  selector: 'app-login-page',
  imports: [],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css'
})
export class LoginPage {
  user:UserModel = {
    username: '',
    password: ''
  };

  constructor(private userService:UserDataService){}
  

  onLoginClick(email: string, password: string) {
    this.user.username = email;
    this.user.password = password;
    this.userService.validateUser(this.user);
    
  }
}
