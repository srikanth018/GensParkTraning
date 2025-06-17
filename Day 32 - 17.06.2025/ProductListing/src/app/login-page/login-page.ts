import { Component, OnInit } from "@angular/core";
import { UserLoginModel } from "../models/UserLoginModel";
import { UserService } from "../services/UserService";
import { FormsModule } from "@angular/forms";
import { Router, RouterOutlet } from "@angular/router";

@Component({
  selector: "app-login-page",
  imports: [FormsModule,RouterOutlet],
  templateUrl: "./login-page.html",
  styleUrl: "./login-page.css",
})
export class LoginPage {
  userLoginData: UserLoginModel = new UserLoginModel();

  constructor(private userService: UserService, private router:Router) {}
  handleLogin() {
    console.log(`from handle login ${this.userLoginData.username},${this.userLoginData.password}`)
    this.userService.validateUserLogin(this.userLoginData);
    this.router.navigate(['home', 'products']);

  }
}
