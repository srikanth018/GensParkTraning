import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/ProductService';
import { CurrencyPipe, DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import { CartService } from '../../services/CartService';
import { AddToCartModel } from '../../Models/AddToCartModel';

@Component({
  selector: 'app-home',
  imports: [CurrencyPipe, NgFor, NgIf, NgClass],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  productList: any = [];
  cartProductIds: number[] = [];
  constructor(
    private productService: ProductService,
    private cartService: CartService
  ) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts().subscribe({
      next: (data: any) => {
        this.productList = data.$values || [];
        console.log('data');

        console.log(this.productList);
      },
      error: (error) => {
        console.error('Error fetching products:', error);
      },
    });
    this.cartService.currentCartProductIds.subscribe((ids: number[]) => {
      this.cartProductIds = ids;
    });
  }
  checkProductInCart(productId: number): boolean {
    return this.cartProductIds.includes(productId);
  }


  addToCart(productId: number) {
    if (!this.checkProductInCart(productId)) {
      let cartItem = new AddToCartModel(productId, 1, 1);
      console.log(cartItem);

      this.cartService.addToCart(cartItem).subscribe({
        next: (data) => {
          let currentCount = this.cartService.cartCount.getValue();
          let currentProductIds = this.cartService.cartProductIds.getValue(); 
          currentProductIds.push(productId);

          this.cartService.updateCartCount(currentCount + 1, currentProductIds);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
}
