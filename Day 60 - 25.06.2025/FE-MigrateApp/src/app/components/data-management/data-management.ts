import { CurrencyPipe, NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ProductService } from '../../services/ProductService';

@Component({
  selector: 'app-data-management',
  imports: [NgFor, NgIf,FormsModule ],
  templateUrl: './data-management.html',
  styleUrl: './data-management.css'
})
export class DataManagement implements OnInit{

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.getAllCategories();
    this.getAllColors();
  }
  categoryList: any[] = [];
  getAllCategories() {
    this.productService.getAllCategories().subscribe({
      next: (data: any) => {
        this.categoryList = data.$values || [];
        this.categoryList.sort((a, b) => b.categoryId - a.categoryId);
        console.log('Categories fetched:', this.categoryList);
      },
      error: (error) => {
        console.error('Error fetching categories:', error);
      }
    });
  }

  categoryName: string = '';
  addCategory(form: NgForm) {
    
    if (form.valid) {
      this.productService.addCategory(this.categoryName as string).subscribe({
        next: (data: any) => {
          console.log('Category added:', data);
          this.getAllCategories();
          form.resetForm();
        },
        error: (error) => {
          console.error('Error adding category:', error);
        }
      });
    }
  }

  editCategoryName: string = '';
  editCategoryId: number | null = null;
  editCategory(category: any) {
    this.editCategoryName = category.name;
    this.editCategoryId = category.categoryId;
  }
  cancelEditing() {
  this.editCategoryId = null;
  this.editCategoryName = '';
}

  updateCategory(categoryId: number, newName: string) {
    if (newName.trim() === '') {
      console.error('Category name cannot be empty');
      return;
    }
    this.productService.updateCategory(categoryId, newName).subscribe({
      next: (data: any) => {
        console.log('Category updated:', data);
        this.getAllCategories();
        this.cancelEditing();
      },
      error: (error) => {
        console.error('Error updating category:', error);
      }
    });
  }

  deleteCategory(categoryId: number) {
    if (confirm('Are you sure you want to delete this category?')) {
      this.productService.deleteCategory(categoryId).subscribe({
        next: (data: any) => {
          console.log('Category deleted:', data);
          this.getAllCategories();
        },
        error: (error) => {
          console.error('Error deleting category:', error);
        }
      });
    }
  }

  colorList:any[]= []
  getAllColors(){
      this.productService.getAllColors().subscribe({
        next:(value:any) => {
          this.colorList = value.$values || [];
          this.colorList.sort((a, b) => b.colorId - a.colorId);
          console.log('Colors fetched:', this.colorList);
        },
        error: (error) => {
          console.error('Error fetching colors:', error);
        }
      })
  }

  addColor(colorName:string){
    this.productService.addColor(colorName).subscribe({
      next:(value) => {
        console.log('Color added:', value);
        this.getAllColors();
        this.colorName = '';
      },
      error: (error) => {
        console.error('Error adding color:', error);
      }
    })
  }
colorName: string = '';
  editColorName: string = '';
  editColorId: number | null = null;

  editColor(color: any) {
    this.editColorName = color.color1;
    this.editColorId = color.colorId;
  }

  cancelColorEditing() {
    this.editColorId = null;
    this.editColorName = '';
  }

  updateColor(colorId: number, newName: string) {
    if (newName.trim() === '') {
      console.error('Color name cannot be empty');
      return;
    }
    this.productService.updateColor(colorId, newName).subscribe({
      next: (data: any) => {
        console.log('Color updated:', data);
        this.getAllColors();
        this.cancelColorEditing();
      },
      error: (error) => {
        console.error('Error updating color:', error);
      }
    });
  }

  deleteColor(colorId: number) {
    if (confirm('Are you sure you want to delete this color?')) {
      this.productService.deleteColor(colorId).subscribe({
        next: (data: any) => {
          console.log('Color deleted:', data);
          this.getAllColors();
        },
        error: (error) => {
          console.error('Error deleting color:', error);
        }
      });
    }
  }

}
