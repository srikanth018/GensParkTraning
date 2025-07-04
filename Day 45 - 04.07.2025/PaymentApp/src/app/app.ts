import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PayemntForm } from "./payemnt-form/payemnt-form";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, PayemntForm],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'PaymentApp';
}
