import { Component } from '@angular/core';
import { CompletedQuiz } from '../../models/CompletedQuiz';
import { CompletedQuizService } from '../../services/CompletedQuizService';
import { AuthService } from '../../services/AuthService';
import { NgFor } from '@angular/common';
import { QuizService } from '../../services/QuizService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-quiz-history',
  imports: [NgFor],
  templateUrl: './quiz-history.html',
  styleUrl: './quiz-history.css'
})
export class QuizHistory {
  completedQuizzes: CompletedQuiz[] = [];

  constructor(private completedQuizService: CompletedQuizService, private authService: AuthService, private quizService: QuizService, private router: Router) {
    this.loadCompletedQuizzes();
  }


  loadCompletedQuizzes() {
    const token = localStorage.getItem('access_token');
    const studentEmail = this.authService.decodeToken(token || '')?.nameid;
    if (studentEmail) {
      this.completedQuizService.getCompletedQuizByStudentEmail(studentEmail).subscribe(
        {
          next: (data) => {
            this.completedQuizzes = data && data.$values ? data.$values : [];
            this.getQuizData();
            console.log('Completed quizzes:', this.completedQuizzes as CompletedQuiz[]);
          },
          error: (error) => {
            console.error('Error fetching completed quizzes:', error);
          }
        }
      );
    }
  }

  getQuizData(){
    this.completedQuizzes.forEach((quiz) => {
      this.quizService.getQuizById(quiz.quizId).subscribe({
        next: (data) => {
          quiz.quizData = data;
          console.log('Quiz data for completed quiz:', this.completedQuizzes);
        },
        error: (error) => {
          console.error('Error fetching quiz data:', error);
        }
      });
    });
  }

  viewCompletedQuiz(completedQuizId:string) {
    this.router.navigate([`/main/quiz-history/${completedQuizId}`]);
  }
}
