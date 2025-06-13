import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RecipeList } from "./recipielist/recipielist";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RecipeList],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'RecipeListApp';
}
