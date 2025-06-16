import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { ProductService } from '../services/productService';
import { ProductModel } from '../models/productModel';
import { Product } from '../product/product';
import '@lottiefiles/dotlottie-wc';
import { SearchService } from '../services/SearchService';
import { debounce, debounceTime, Subject, Subscription, switchMap, tap } from 'rxjs';


@Component({
  selector: 'app-products',
  standalone: true,
  imports: [Product],
  templateUrl: './products.html',
  styleUrls: ['./products.css'],
})
export class Products implements OnInit, OnDestroy {
  products: ProductModel[] = [];
  limit = 10;
  skip = 0;
  loading = false;
  total = 0;
  currentterm = '';
  private searchSubject = new Subject<string>();
  private searchSubscription?: Subscription;

  constructor(private productService: ProductService, private searchService: SearchService) {}

  ngOnInit(): void {
    this.loadProductData(); 

    // Handle search
    this.searchSubscription = this.searchSubject.pipe(
      debounceTime(400),
      tap(() => {
        this.loading = true;
        this.skip = 0;
        this.products = []; // Clear old results when searching
      }),
      switchMap(query => this.productService.getProductSearchResult(query, this.limit, this.skip)),
    ).subscribe({
      next: (data: any) => {
        this.products = data.products as ProductModel[];
        this.total = data.total;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });

    // Listen to search terms
    this.searchService.search$.subscribe(term => {
      this.currentterm = term;
      this.searchSubject.next(term);
    });
  }

  ngOnDestroy(): void {
    this.searchSubscription?.unsubscribe();
  }

  @HostListener('window:scroll', [])
  onScroll(): void {
    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight - 100;
    if (scrollPosition >= threshold && this.products.length < this.total) {
      this.loadMoreProductData();
    }
  }

  loadProductData(): void {
    this.loading = true;
    const serviceCall = this.currentterm
      ? this.productService.getProductSearchResult(this.currentterm, this.limit, this.skip)
      : this.productService.getAllProducts(this.limit, this.skip);

    serviceCall.subscribe({
      next: (data: any) => {
        this.products = data.products as ProductModel[];
        this.total = data.total;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }

  loadMoreProductData(): void {
    this.loading = true;
    this.skip += this.limit;
    
    const serviceCall = this.currentterm
      ? this.productService.getProductSearchResult(this.currentterm, this.limit, this.skip)
      : this.productService.getAllProducts(this.limit, this.skip);

    serviceCall.subscribe({
      next: (data: any) => {
        this.products = [...this.products, ...data.products];
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }
}
