import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/CartService';
import { CurrencyPipe, NgClass, NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-cart',
  imports: [NgIf, NgFor, CurrencyPipe],
  templateUrl: './cart.html',
  styleUrl: './cart.css',
})
export class Cart implements OnInit {
  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCartItems();
  }

  cartList: any = [];

  loadCartItems() {
    this.cartService.getCartItems(1).subscribe({
      next: (data: any) => {
        this.cartList = data.$values;
        console.log(this.cartList);
      },
      error: (err) => {
        this.cartList=[];
        console.log(err);
      },
    });
  }

  removeFromCart(cartId: number, productId:number) {
    this.cartService.removeFromCart(cartId).subscribe({
      next: (value) => {
        console.log(value);
        let currentCount = this.cartService.cartCount.getValue();
        let currentProductIds = this.cartService.cartProductIds.getValue();
        currentProductIds = currentProductIds.filter(p => p != productId);
        this.cartService.updateCartCount(currentCount - 1, currentProductIds);
        this.loadCartItems();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
