import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/ProductService';
import { CurrencyPipe, DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import { CartService } from '../../services/CartService';
import { AddToCartModel } from '../../Models/AddToCartModel';

@Component({
  selector: 'app-home',
  imports: [CurrencyPipe, NgFor, NgIf, NgClass],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  productList: any[] = [];
  allProducts: any[] = [];
  cartProductIds: number[] = [];
  constructor(
    private productService: ProductService,
    private cartService: CartService
  ) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts().subscribe({
      next: (data: any) => {
        this.productList = data.$values || [];
        this.allProducts = [...this.productList];
        this.getAllCategories();
      },
      error: (error) => {
        console.error('Error fetching products:', error);
      },
    });
    this.cartService.currentCartProductIds.subscribe((ids: number[]) => {
      this.cartProductIds = ids;
    });
  }
  checkProductInCart(productId: number): boolean {
    return this.cartProductIds.includes(productId);
  }


  addToCart(productId: number) {
    if (!this.checkProductInCart(productId)) {
      let cartItem = new AddToCartModel(productId, 1, 1);
      console.log(cartItem);

      this.cartService.addToCart(cartItem).subscribe({
        next: (data) => {
          let currentCount = this.cartService.cartCount.getValue();
          let currentProductIds = this.cartService.cartProductIds.getValue(); 
          currentProductIds.push(productId);

          this.cartService.updateCartCount(currentCount + 1, currentProductIds);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  categoryList: string[] = [];
  getAllCategories() {
  const unique = new Set<string>();
  this.productList.forEach((product: any) => {
    if (product.category) {
      unique.add(product.category);
    }
  });
  this.categoryList = Array.from(unique);
}

  selectedCategory: string = 'All';
  filterByCategory(event: Event) {
    const category = (event.target as HTMLSelectElement).value;
    if (category === 'All') {
      this.productList = this.allProducts;
    } else {
      this.selectedCategory = category;
      this.productList = this.allProducts.filter(
        (product) => product.category === category
      );
    }
  }
}
