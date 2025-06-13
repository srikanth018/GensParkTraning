import { Component } from '@angular/core';
import { UserDataService } from '../services/user-data-service';

@Component({
  selector: 'app-user-page',
  imports: [],
  templateUrl: './user-page.html',
  styleUrl: './user-page.css'
})
export class UserPage {
  username$:any;
  username:string = "";

  constructor(private userService:UserDataService){
    this.userService.username$.subscribe({
      next:(value)=>{
        this.username = value ?? "";
      },
      error:(err)=>{
        alert(err);
      }
    })
  }

  
}
