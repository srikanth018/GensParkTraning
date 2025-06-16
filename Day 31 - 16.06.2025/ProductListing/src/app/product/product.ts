import { Component, Input } from '@angular/core';
import { ProductModel } from '../models/productModel';

@Component({
  selector: 'app-product',
  imports: [],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
    @Input() product:ProductModel = new ProductModel();
}
