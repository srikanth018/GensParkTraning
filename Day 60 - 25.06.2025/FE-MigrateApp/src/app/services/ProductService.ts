import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable()
export class ProductService{
    private http = inject(HttpClient);

    private baseUrl = 'http://localhost:5041/api/Products';

    getProducts() {
        return this.http.get(this.baseUrl);
    }
}