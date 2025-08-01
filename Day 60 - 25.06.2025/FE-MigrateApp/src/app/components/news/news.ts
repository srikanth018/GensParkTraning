import { Component, OnInit } from '@angular/core';
import { NewsService } from '../../services/NewsService';
import { DatePipe, NgFor, NgIf, SlicePipe } from '@angular/common';
import {
  FormGroup,
  Validators,
  FormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { UpdateNewsModel } from '../../Models/UpdateNewsModel';
import { CreateNews } from "../create-news/create-news";

@Component({
  selector: 'app-news',
  imports: [NgIf, NgFor, SlicePipe, DatePipe, ReactiveFormsModule, CreateNews],
  templateUrl: './news.html',
  styleUrl: './news.css',
})
export class News implements OnInit {
  newsList: any[] = [];

  constructor(private newsService: NewsService, private fb: FormBuilder) {
    this.newsForm = this.fb.group({
      title: ['', Validators.required],
      shortDescription: ['', Validators.required],
      image: ['', [Validators.required, Validators.pattern('https?://.+')]],
      content: ['', Validators.required],
      status: [1, Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadNews();
  }

  loadNews() {
    this.newsService.getNews().subscribe({
      next: (data: any) => {
        this.newsList = data.$values || [];
        this.newsList.sort((a, b) => b.newsId - a.newsId);
      },
      error: (error) => {
        console.error('Error fetching news:', error);
      },
    });
  }

  isEditModalOpen = false;
  editingNews: any = null;
  isSaving = false;

  newsForm: FormGroup;
  openEditModal(newsItem: any): void {
    this.editingNews = newsItem;
    this.newsForm.patchValue({
      title: newsItem.title,
      shortDescription: newsItem.shortDescription,
      image: newsItem.image,
      content: newsItem.content,
      status: newsItem.status,
    });
    this.isEditModalOpen = true;
  }
  closeEditModal(): void {
    this.isEditModalOpen = false;
    this.editingNews = null;
    this.newsForm.reset();
    this.isSaving = false;
  }

  afterCreateNews() {
    this.isCreateNewsModalOpen = false;
    this.loadNews();
    this.closeEditModal();
  }

  saveNews(): void {
    if (this.newsForm.invalid) return;

    this.isSaving = true;
    let newsData = new UpdateNewsModel(
      this.editingNews.userId,
      this.newsForm.value.title,
      this.newsForm.value.shortDescription,
      this.newsForm.value.image,
      this.newsForm.value.content,
      this.editingNews.createdDate,
      this.editingNews.status
    );
    this.newsService.updateNews(this.editingNews.newsId, newsData).subscribe({
      next: () => {
        this.loadNews();
        this.closeEditModal();
      },
      error: (error) => {
        console.error('Error updating news:', error);
      },
    });
  }

  deleteNews(newsId: number): void {
    if (confirm('Are you sure you want to delete this news item?')) {
      this.newsService.deleteNews(newsId).subscribe({
        next: () => {
          this.loadNews();
        },
        error: (error) => {
          console.error('Error deleting news:', error);
        },
      });
    }
  }

  isCreateNewsModalOpen = false;
  createNew(){
    this.isCreateNewsModalOpen = true;
  }
}
