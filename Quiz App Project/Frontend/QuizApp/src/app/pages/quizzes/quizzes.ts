import { Component } from '@angular/core';
import { QuizService } from '../../services/QuizService';
import { Question } from "../../components/question/question";
import { QuizCard } from "../../components/quiz-card/quiz-card";
import { NgFor, NgIf } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Loading } from '../../components/loading/loading';

@Component({
  selector: 'app-quizzes',
  imports: [NgFor, NgIf, QuizCard, RouterOutlet, Loading],
  templateUrl: './quizzes.html',
  styleUrl: './quizzes.css'
})
export class Quizzes {
  quizzes: any[] = [];
  filteredQuizzes: any[] = [];
  isLoading: boolean = false;

  constructor(private quizService: QuizService) {}

  ngOnInit() {
    this.loadQuizzes();
  }
  loadQuizzes() {
    this.isLoading = true;
    this.quizService.getAllQuizzes().subscribe({
      next: (data) => {
        this.quizzes = data.$values;
        this.filteredQuizzes = this.quizzes; 
        console.log('Fetched quizzes:', this.quizzes);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching quizzes:', err);
        this.isLoading = false;
      }
    });
  }

  
}
