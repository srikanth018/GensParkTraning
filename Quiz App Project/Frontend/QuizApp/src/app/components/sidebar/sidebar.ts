import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/AuthService';

@Component({
  selector: 'app-sidebar',
  imports: [RouterModule],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar implements OnInit {

  constructor(private authService:AuthService) {}
  userRole: string | null = '';

  ngOnInit() {
    this.userRole = this.getRole();
  }

  logout(){
    localStorage.removeItem('access_token');
    window.location.reload();
  }


  getRole(){
    const token = localStorage.getItem('access_token');
    if (token) {
      const user = this.authService?.decodeToken(token);
      return user?.role;
    }
    return null;
  }
}
