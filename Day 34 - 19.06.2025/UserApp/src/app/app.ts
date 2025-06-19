import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserData } from "./user-data/user-data";
import { AddUser } from "./add-user/add-user";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UserData, AddUser],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'UserApp';
}
