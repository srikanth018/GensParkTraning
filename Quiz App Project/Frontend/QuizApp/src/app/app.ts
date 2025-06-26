import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgApexchartsModule } from 'ng-apexcharts';
import { Sample } from "./components/sample/sample";
import { Success } from "./components/success/success";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgApexchartsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected title = 'QuizApp';
}
