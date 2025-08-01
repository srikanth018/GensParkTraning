import { CurrencyPipe, NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  NgForm,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { PlaceOrderModel } from '../../Models/PlaceOrderModel';

@Component({
  selector: 'app-buynow-form',
  imports: [ReactiveFormsModule, NgIf, CurrencyPipe, NgFor],
  templateUrl: './buynow-form.html',
  styleUrl: './buynow-form.css',
})
export class BuynowForm implements OnInit {
  @Input() cartList: any[] = [];
  @Output() orderData = new EventEmitter<any>();
  orderForm!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.orderForm = this.fb.group({
      customerName: ['', Validators.required],
      customerPhone: [
        '',
        [Validators.required, Validators.pattern('^[0-9]{10}$')],
      ],
      customerEmail: ['', [Validators.required, Validators.email]],
      customerAddress: ['', Validators.required],
      userId: [0, Validators.required],
    });

    this.consolidateCartItems();
  }

//   customerAddress
// : 
// "salme"
// customerEmail
// : 
// "srikanth@gmail.com"
// customerName
// : 
// "Srikanth"
// customerPhone
// : 
// "1234567890"
// userId
// : 
// 0

isPlacingOrder : boolean = false;
  placeOrder() {
    this.isPlacingOrder = true;
    if (this.orderForm.valid) {
      const dto = this.orderForm.value;
      var totalAmount = this.totalPayableAmount;
      var placeOrderModel = new PlaceOrderModel(
        dto.customerName,
        dto.customerPhone,
        dto.customerEmail,
        dto.customerAddress,
        totalAmount,
        'Cash On Delivery',
        1
      );
      
      this.orderData.emit(placeOrderModel);

    } else {
      this.orderForm.markAllAsTouched();
    }
  }

  totalPayableAmount: any = 0.0;
  calculatePayableAmount() {
    this.totalPayableAmount = this.cartList.reduce((sum, item) => {
      return sum + item.totalPrice;
    }, 0);
  }

  consolidateCartItemsList: any[] = [];

  consolidateCartItems() {
    this.calculatePayableAmount();
    let consolidatedList: any[] = [];
    consolidatedList.push({ totalPayableAmount: this.totalPayableAmount });
    consolidatedList.push({ productDetails: [] });

    this.cartList.forEach((item) => {
      let productData: any = {};
      productData['name'] = item.product.productName;
      productData['productPrice'] = item.product.price;
      productData['quantity'] = item.quantity;
      productData['totalPrice'] = item.totalPrice;

      consolidatedList[1].productDetails.push(productData);
    });
    this.consolidateCartItemsList = consolidatedList;
  }
}
