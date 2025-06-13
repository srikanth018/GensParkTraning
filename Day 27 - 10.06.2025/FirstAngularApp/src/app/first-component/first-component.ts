import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-first-component',
  imports: [FormsModule],
  templateUrl: './first-component.html',
  styleUrl: './first-component.css'
})
export class FirstComponent {
  name:string;
  saveHeartClass:string = "bi bi-bookmark-heart";
  
  constructor(){
    this.name = "Ramu"
  }
  onButtonClick(uname:string){
    this.name = uname;
  }
}
