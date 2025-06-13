import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../services/AuthService';

@Component({
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar implements OnInit {

  private authService = inject(AuthService);

  userData:any;
  ngOnInit(): void {
    this.userData = this.authService.decodeToken(localStorage.getItem('access_token') || '');
  }
  
}
