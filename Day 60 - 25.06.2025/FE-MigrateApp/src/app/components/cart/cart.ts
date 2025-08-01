import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/CartService';
import {
  CurrencyPipe,
  DecimalPipe,
  NgClass,
  NgFor,
  NgIf,
} from '@angular/common';
import { UpdateQuantityModel } from '../../Models/UpdateQuantityModel';
import { BuynowForm } from '../buynow-form/buynow-form';
import { PlaceOrderModel } from '../../Models/PlaceOrderModel';

@Component({
  selector: 'app-cart',
  imports: [NgIf, NgFor, CurrencyPipe, BuynowForm, DecimalPipe],
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
        this.calculatePayableAmount();
      },
      error: (err) => {
        this.cartList = [];
        console.log(err);
      },
    });
  }

  removeFromCart(cartId: number, productId: number) {
    this.cartService.removeFromCart(cartId).subscribe({
      next: (value) => {
        console.log(value);
        let currentCount = this.cartService.cartCount.getValue();
        let currentProductIds = this.cartService.cartProductIds.getValue();
        currentProductIds = currentProductIds.filter((p) => p != productId);
        this.cartService.updateCartCount(currentCount - 1, currentProductIds);
        this.loadCartItems();
        this.totalPayableAmount = 0.0;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  errorMessage: string = '';
  errorMessageProductId: number = 0;

  increment(curQuan: number, cartId: number, productId: number) {
    this.errorMessage = '';
    this.errorMessageProductId = 0;
    this.updateQuantity(cartId, productId, curQuan + 1);
  }

  decrement(curQuan: number, cartId: number, productId: number) {
    if (curQuan - 1 <= 0) {
      this.errorMessageProductId = productId;
      this.errorMessage = 'Decrease the quantity below 1 is not allowed.';
    } else {
      this.errorMessageProductId = 0;
      this.errorMessage = '';
      this.updateQuantity(cartId, productId, curQuan - 1);
    }
  }

  updateQuantity(cartId: number, productId: number, quantity: number) {
    var updateData = new UpdateQuantityModel(productId, quantity, cartId);
    this.cartService.updateQuantity(updateData).subscribe({
      next: (value) => {
        var item = this.cartList.find(
          (i: any) => i.productId === productId && i.cartId === cartId
        );
        if (item) {
          item.quantity = quantity;
          item.totalPrice = item.product.price * quantity;
        }
        this.calculatePayableAmount();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  isOrderNowFormOpened: boolean = false;
  buyNow() {
    this.isOrderNowFormOpened = true;
  }

  placeOrder(orderData: PlaceOrderModel) {
    this.cartService.placeOrder(orderData).subscribe({
      next: (value) => {
        this.isOrderNowFormOpened = false;
        console.log(value);
        this.cartList = [];
        this.totalPayableAmount = 0.0;
        this.cartService.updateCartCount(0, []);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  totalPayableAmount: any = 0.0;
  calculatePayableAmount() {
    this.totalPayableAmount = this.cartList.reduce((sum: any, item: any) => {
      return sum + item.totalPrice;
    }, 0);
  }
}
