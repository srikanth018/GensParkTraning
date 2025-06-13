import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-details',
  imports: [CommonModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails {
  Products = [
    {
      Title: 'Apple Laptop',
      Description: 'Apple MacBook Air 13.3 i5 1.8/8GB/128GB SSD Laptop ',
      ImgUrl: 'https://www.tradeinn.com/f/13735/137354178_5/apple-macbook-air-13.3-i5-1.8-8gb-128gb-ssd-laptop.webp',
    },
    {
      Title: 'Dell Laptop',
      Description: 'DELL Inspiron 15 3000 AMD Ryzen 4GB DDR4 1 TB 15.6 Windows 10 - Accent Black Laptop',
      ImgUrl: 'https://images.jdmagicbox.com/quickquotes/images_main/dell-laptops-11-06-2021-002-227393979-p9c7w.jpg',
    },
    {
      Title: 'HP Laptop',
      Description: 'HP Pavilion 13.3 FHD Laptop, Intel Core i3, 8GB RAM, India',
      ImgUrl: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQNPkIzbCliA21Ho9XE-gmP2mHiHwubtJjlww&s',
    },
  ];
  cartBtn:string = "bi bi-cart";
  cartCount: number = 0;

  oncartClick(){
    ++this.cartCount;
  }
}
