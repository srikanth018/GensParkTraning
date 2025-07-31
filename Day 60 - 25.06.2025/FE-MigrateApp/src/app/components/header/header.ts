import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { CartService } from '../../services/CartService';

@Component({
  selector: 'app-header',
  imports: [RouterOutlet, RouterModule],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header implements OnInit {

  constructor(private cartService: CartService) {}
  cartItemCount: number = 0;
  cartProductIds: number[] = [];
  ngOnInit(): void {
    this.cartService.getCartItems(1).subscribe(
      {
        next: (items:any) => {
          this.cartService.updateCartCount(items.$values.length, items.$values.map((item: any) => item.productId));
        },
        error: (error) => {
          console.error('Error fetching cart items:', error);
        }
      }
    );
    this.cartService.currentCartCount.subscribe(count => {
      this.cartItemCount = count;
    });
  }



}
