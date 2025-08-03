import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable()
export class ProductService{
    private http = inject(HttpClient);

    private baseUrl = 'http://localhost:5041/api/Products';

    getProducts() {
        return this.http.get(this.baseUrl);
    }
    getProductById(id: number) {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    // Categories

    getAllCategories() {
        return this.http.get(`http://localhost:5041/api/Category`);
    }
    addCategory(name: string) {
        return this.http.post(`http://localhost:5041/api/Category`, {name});
    }
    updateCategory(categoryId: number, name: string) {
        return this.http.put(`http://localhost:5041/api/Category/${categoryId}`, {name});
    }
    deleteCategory(categoryId: number) {
        return this.http.delete(`http://localhost:5041/api/Category/${categoryId}`);
    }

    // Colors

    getAllColors(){
        return this.http.get(`http://localhost:5041/api/Color`);
    }
    addColor(color1:string){
        return this.http.post(`http://localhost:5041/api/Color`, {color1});
    }
    updateColor(colorId: number, color1: string) {
        return this.http.put(`http://localhost:5041/api/Color/${colorId}`, {color1});
    }
    deleteColor(colorId: number) {
        return this.http.delete(`http://localhost:5041/api/Color/${colorId}`);
    }

}