import { Component } from '@angular/core';
import { FirstComponent } from "./first-component/first-component";
import { CustomerDetails } from "./customer-details/customer-details";
import { ProductDetails } from "./product-details/product-details";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [FirstComponent, CustomerDetails, ProductDetails]
})
export class App {
  protected title = 'FirstAngularApp';
}
