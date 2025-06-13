import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable()
export class RecipeService {
  constructor(private http: HttpClient) {}

  getRecipes() {
    return this.http.get('https://dummyjson.com/recipes');
  }
}
