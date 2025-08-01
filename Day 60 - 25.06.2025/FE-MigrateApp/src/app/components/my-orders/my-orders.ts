import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/OrderService';
import { DatePipe, DecimalPipe, NgClass, NgFor, NgIf } from '@angular/common';
import { ProductService } from '../../services/ProductService';
import { CartService } from '../../services/CartService';

@Component({
  selector: 'app-my-orders',
  imports: [DatePipe, NgIf, NgFor, DecimalPipe, NgClass],
  templateUrl: './my-orders.html',
  styleUrl: './my-orders.css',
})
export class MyOrders implements OnInit {
  constructor(private orderService: OrderService, private productService: ProductService, private cartService: CartService) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  orders: any[] = [];
  isOrderDetailsOpen: boolean = false;
  selectedOrder: any = null;
  selectedOrderProducts: any[] = [];

  loadOrders() {
    this.orderService.getOrdersByUserId(1).subscribe({
      next: (data: any) => {
        this.orders = data?.$values ?? [];
        
        
      },
      error: (error: any) => {
        console.error('Error loading orders:', error);
      },
    });
  }

  viewOrderDetails(orderId: number) {
    this.selectedOrder = this.orders.find(order => order.orderID === orderId);
    if (!this.selectedOrder) return;

    const details = this.selectedOrder.orderDetails?.$values ?? [];
    const productDetailsPromises = details.map((item: any) =>
      this.productService.getProductById(item.productId).toPromise().then(product => ({
        ...item,
        product,
        amount: item.quantity * item.price
      }))
    );

    Promise.all(productDetailsPromises).then(products => {
      this.selectedOrderProducts = products;
      this.isOrderDetailsOpen = true;
      console.log('Selected Order:', products);
    });

  }

  closePopup() {
    this.isOrderDetailsOpen = false;
    this.selectedOrder = null;
    this.selectedOrderProducts = [];
  }


}

