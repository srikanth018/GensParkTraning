import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterOutlet } from '@angular/router';
import { SearchService } from '../services/SearchService';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterOutlet, FormsModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  searchTerm: string = '';
  constructor(private searchService: SearchService) {}

  onSearch() {
    this.searchService.updateSearch(this.searchTerm);
  }
}
