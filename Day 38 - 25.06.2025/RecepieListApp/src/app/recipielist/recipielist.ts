import { Component, inject, signal } from '@angular/core';
import { RecipeService } from '../services/recipe.service';
import { RecipeModel } from '../models/recipe.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-recipielist',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './recipielist.html',
  styleUrl: './recipielist.css'
})
export class RecipeList {
  recipes = signal<RecipeModel[]>([]);
  loading = signal(true);
  private recipeService = inject(RecipeService);

  constructor() {
    this.recipeService.getRecipes().subscribe({
      next: (res: any) => {
        this.recipes.set(res.recipes as RecipeModel[]);
        this.loading.set(false);
      },
      error: (err) => {
        console.error(err);
        this.loading.set(false);
      }
    });
  }
}
