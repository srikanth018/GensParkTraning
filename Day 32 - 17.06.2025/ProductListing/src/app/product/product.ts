import { Component, Input } from '@angular/core';
import { ProductModel } from '../models/productModel';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product',
  imports: [CommonModule],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
  constructor(private router:Router){}
    @Input() product:ProductModel = new ProductModel();
      showFull: boolean = false;

  toggleReadMore() {
    this.showFull = !this.showFull;
  }

  viewProduct(id:number){
    this.router.navigate(['home','product',id]);
  }

}
