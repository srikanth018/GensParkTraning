import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginPage } from "./login-page/login-page";
import { UserPage } from "./user-page/user-page";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoginPage, UserPage],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'UserLogin';
}
