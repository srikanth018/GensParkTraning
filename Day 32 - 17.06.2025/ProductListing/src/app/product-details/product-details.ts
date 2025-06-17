import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../services/productService';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  imports: [CommonModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css'
})
export class ProductDetails implements OnInit {
  constructor(private productService:ProductService){}
    route = inject(ActivatedRoute);
  id:number = this.route.snapshot.params["id"];
  ngOnInit(): void {
    this.onShowProductDetails(this.id);
  }


  productData:any = {};


  onShowProductDetails(id:number){
    this.productService.getProduct(id).subscribe({
      next:(data:any)=>{
        this.productData = data;
      }
    })
  }
}
