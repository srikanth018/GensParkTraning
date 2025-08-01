import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
    private http = inject(HttpClient);

    private baseUrl = 'http://localhost:5041/api/Order/';

    getOrdersByUserId(userId:number){
        return this.http.get(`${this.baseUrl}user/${userId}`);
    }
}

