import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { QuizService } from '../../services/QuizService';
import { QuizResponseMapper } from '../../misc/QuizResponseMapper';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-view-quiz-student',
  imports: [NgIf, RouterOutlet],
  templateUrl: './view-quiz-student.html',
  styleUrl: './view-quiz-student.css',
})
export class ViewQuizStudent implements OnInit {
  constructor(
    private router: ActivatedRoute,
    private quizService: QuizService,
    private route: Router
  ) {}

  quizId: string = '';
  quiz: any;
  ngOnInit(): void {
    this.quizId = this.router.snapshot.paramMap.get('id') || '';
    this.getQuizData();
  }

  getQuizData() {
    this.quizService.getQuizById(this.quizId).subscribe({
      next: (data) => {
        this.quiz = QuizResponseMapper.mapResponseToQuiz(data);
        this.quiz.timeLimit = this.timespanToMinutes(this.quiz.timeLimit);
        console.log(this.quiz);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  attempQuiz() {
    this.route.navigate(['attempt-quiz', this.quizId]);
  }
  timespanToMinutes(timeSpan: string): number {
    const [hours, minutes, seconds] = timeSpan.split(':').map(Number);
    return hours * 60 + minutes + Math.floor(seconds / 60);
  }
}
