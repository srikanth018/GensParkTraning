import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { AddToCartModel } from "../Models/AddToCartModel";
import { UpdateQuantityModel } from "../Models/UpdateQuantityModel";
import { PlaceOrderModel } from "../Models/PlaceOrderModel";

@Injectable({
  providedIn: 'root'
})
export class CartService {
    private http = inject(HttpClient);

    private baseUrl = 'http://localhost:5041/api/ShoppingCart/';
    
    cartCount = new BehaviorSubject<number>(0);
    currentCartCount = this.cartCount.asObservable();

    cartProductIds = new BehaviorSubject<number[]>([]);
    currentCartProductIds = this.cartProductIds.asObservable();
    updateCartCount(count: number, productIds: number[] = []) {
        this.cartCount.next(count);
        this.cartProductIds.next(productIds);
    }

    getCartItems(userId: number) {
        return this.http.get(`${this.baseUrl}user/${userId}`);
    }

    addToCart(cartData: AddToCartModel){        
        return this.http.post(`${this.baseUrl}add`,cartData);
    }

    removeFromCart(cartId:number){
        return this.http.delete(`${this.baseUrl}remove/${cartId}`);
    }

    updateQuantity(updateData:UpdateQuantityModel){
        return this.http.put(`${this.baseUrl}update-quantity`,updateData);
    }

    placeOrder(orderData:PlaceOrderModel){
        return this.http.post(`${this.baseUrl}place-order`, orderData);
    }
}