import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { VideosList } from "./components/videos-list/videos-list";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, VideosList],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'FE-VideoStreamApp';
}
