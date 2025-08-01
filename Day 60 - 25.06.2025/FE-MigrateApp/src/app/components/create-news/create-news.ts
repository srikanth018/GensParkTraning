import { NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NewsService } from '../../services/NewsService';
import { CreateNewsModel } from '../../Models/CreateNewsModel';

@Component({
  selector: 'app-create-news',
  imports: [NgIf, ReactiveFormsModule],
  templateUrl: './create-news.html',
  styleUrl: './create-news.css'
})
export class CreateNews {
  @Output() close = new EventEmitter<void>();
  newsForm: FormGroup;

  constructor(private fb: FormBuilder, private newsService: NewsService) {
    this.newsForm = this.fb.group({
      title: ['', Validators.required],
      shortDescription: ['', Validators.required],
      image: ['', [Validators.required, Validators.pattern('https?://.+')]],
      content: ['', Validators.required],
      createdDate: [new Date()],
      status: [1]
    });
  }

  isSaving = false;
  onSubmit() {
    if (this.newsForm.valid) {
      this.isSaving = true;
      let newsData = new CreateNewsModel(
        1,
        this.newsForm.value.title,
        this.newsForm.value.shortDescription,
        this.newsForm.value.image,
        this.newsForm.value.content,
        this.newsForm.value.createdDate,
        this.newsForm.value.status
      )
      console.log('Creating news with data:', newsData);
      
      this.newsService.createNews(newsData)
      .subscribe({
        next: (value) => {
          console.log('News created successfully:', value);
          this.closeEditModal();
        },
        error: (error) => {
          console.error('Error creating news:', error);
        },
      });
    }
  }

  closeEditModal(): void {
    this.close.emit();
    this.newsForm.reset();
    this.isSaving = false;
  }
}
