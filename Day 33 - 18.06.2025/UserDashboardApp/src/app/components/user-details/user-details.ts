import { Component } from '@angular/core';
import { UserService } from '../../services/UserService';
import { RouterOutlet } from '@angular/router';
import { UserModel } from '../../models/UserModel';

@Component({
  selector: 'app-user-details',
  imports: [RouterOutlet],
  templateUrl: './user-details.html',
  styleUrl: './user-details.css'
})
export class UserDetails {
  constructor(private userService: UserService) {}

  userData: UserModel[] = [];

  ngOnInit() {
    this.userService.getAllUsers().subscribe({
      next: (data: any) => {
        this.userData = data.users;
      },
      error: (error: any) => {
        console.error('Error fetching user data:', error);
      }
    });
  }

  filterUsers(key: string, value: string) {
    
    this.userService.getFilteredUsers(key, value).subscribe({
      next: (data: any) => {
        this.userData = data.users;
      },
      error: (error: any) => {
        console.error(`Error filtering users by ${key}=${value}:`, error);
      }
    });
  }
}
