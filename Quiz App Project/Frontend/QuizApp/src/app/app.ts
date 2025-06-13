import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Sample } from "./components/sample/sample";
import { Success } from "./components/success/success";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Sample],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'QuizApp';
}
