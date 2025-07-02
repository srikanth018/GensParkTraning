import { Component, inject, OnInit } from '@angular/core';
import { QuizSignalRService } from '../../components/sidebar/quiz-signalr.service';
import { NgFor } from '@angular/common';
import { DashboardChart } from '../../components/dashboard-chart/dashboard-chart';

@Component({
  selector: 'app-student-dashboard',
  imports: [NgFor, DashboardChart],
  templateUrl: './student-dashboard.html',
  styleUrl: './student-dashboard.css',
})
export class StudentDashboard implements OnInit {
  private quizSignalR = inject(QuizSignalRService);
  quizzes: { category: string; title: string }[] = [];

  ngOnInit(): void {
    this.quizSignalR.startConnection().then(() => {
      console.log('SignalR Connection Established');
    });
    this.quizSignalR.onReceiveNewQuiz((category, title) => {
      console.log('New Quiz Received:', category, title);
      this.quizzes.unshift({ category, title });
    });
  }
}
