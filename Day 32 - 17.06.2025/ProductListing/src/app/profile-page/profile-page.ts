import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/UserService';
import { UserModel } from '../models/UserModel';

@Component({
  selector: 'app-profile-page',
  imports: [],
  templateUrl: './profile-page.html',
  styleUrl: './profile-page.css'
})
export class ProfilePage implements OnInit{
  constructor(private userService:UserService){};
  
  userData:UserModel = new UserModel();
    ngOnInit(): void {
      this.userService.callGetProfile().subscribe({
        next:(data:any)=>{
          this.userData = data as UserModel;
        }
      })
    }
}
