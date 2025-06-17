import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { SearchService } from '../services/SearchService';
import { UserService } from '../services/UserService';
import { UserModel } from '../models/UserModel';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, FormsModule, RouterOutlet],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit{
  searchTerm: string = '';
  constructor(private searchService: SearchService, private userService:UserService, private router:Router) {}

  userData:UserModel = new UserModel();
  ngOnInit(): void {
    this.userService.callGetProfile().subscribe({
      next:(data:any)=>{
        this.userData = data as UserModel;
      }
    })
  }

  onProfileClick(){
    this.router.navigate(['home', 'profile']);
  }

  onSearch() {
    this.searchService.updateSearch(this.searchTerm);
  }
}
