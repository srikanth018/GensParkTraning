import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserDashboard } from "./components/user-dashboard/user-dashboard";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UserDashboard],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'UserDashboardApp';
}
