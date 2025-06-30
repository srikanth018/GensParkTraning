import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/AuthService';
import { QuizService } from '../../services/QuizService';
import { QuizResponseMapper } from '../../misc/QuizResponseMapper';
import { CompletedQuizService } from '../../services/CompletedQuizService';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { QuestionEditPopup } from '../../components/question-edit-popup/question-edit-popup';

@Component({
  selector: 'app-view-quiz-teacher',
  imports: [NgIf, NgFor, QuestionEditPopup],
  templateUrl: './view-quiz-teacher.html',
  styleUrl: './view-quiz-teacher.css',
})
export class ViewQuizTeacher implements OnInit {
  isloading: boolean = false;
  isEditing: boolean = false;
  quizId: string = '';
  quiz: any = [];
  completedQuizzes: any = [];
  averageTimeTaken: string = '';
  constructor(
    private route: ActivatedRoute,
    private quizService: QuizService,
    private completedQuizService: CompletedQuizService
  ) {}
  ngOnInit(): void {
    this.quizId = this.route.snapshot.params['id'];
    this.getQuizData();
    this.getCompletedQuizzes();
  }

  getQuizData() {
    this.isloading = true;
    this.quizService.getQuizById(this.quizId).subscribe({
      next: (data) => {
        this.quiz = QuizResponseMapper.mapResponseToQuiz(data);
        this.quiz.timeLimit = this.timespanToMinutes(this.quiz.timeLimit);
        this.isloading = false;
        console.log(this.quiz);
      },
      error: (err) => {
        console.log(err);
        this.isloading = false;
      },
    });
  }

  getCompletedQuizzes() {
    this.completedQuizService.getCompletedQuizByQuizId(this.quizId).subscribe({
      next: (data: any) => {
        this.completedQuizzes = data.$values;
        this.averageTimeTaken = this.averageTime();
        console.log(data.$values);
      },
      error: (err) => {
        console.log(err);
        return 0;
      },
    });
  }

  averageTime(): string {
    if (this.completedQuizzes?.length === 0) {
      return '';
    }

    const totalTimeTaken = this.completedQuizzes.reduce(
      (acc: number, q: any) => {
        const timeTaken =
          new Date(q.endedAt).getTime() - new Date(q.startedAt).getTime();
        return acc + timeTaken;
      },
      0
    );

    return this.formatTime(totalTimeTaken / this.completedQuizzes.length);
  }

  maxMins(): number {
    const maxMinTaken = this.completedQuizzes.map((q: any) => {
      const ms =
        new Date(q.endedAt).getTime() - new Date(q.startedAt).getTime();
      return Math.floor((ms % 3600) / 60);
    });
    return Math.max(...maxMinTaken);
  }

  formatTime(ms: any): string {
    const totalSeconds = Math.floor(ms / 1000);
    const minutes = Math.floor(totalSeconds / 60);
    return `${minutes}m`;
  }

  editQuestionForm!: FormGroup;
  currentQuestionId: string = '';
  currentQuestionIndex: number = -1;

  editQuestion(quizId: string, questionId: string, questionIndex: number) {
    this.currentQuestionId = questionId;
    this.currentQuestionIndex = questionIndex;
    this.isEditing = true;

    this.editQuestionForm = new FormGroup({
      questionText: new FormControl(
        this.quiz.questions[questionIndex].questionText,
        [Validators.required, Validators.minLength(10)]
      ),
      mark: new FormControl(this.quiz.questions[questionIndex].mark, [
        Validators.required,
        Validators.min(1),
      ]),
      options: new FormArray(
        this.quiz.questions[questionIndex].options.map((option: any) => {
          return new FormGroup({
            optionText: new FormControl(option.optionText, Validators.required),
            isCorrect: new FormControl(option.isCorrect),
          });
        })
      ),
    });
  }

  onSaveEdit() {
    if (this.editQuestionForm.valid) {
      // Update your quiz data here
      const updatedQuestion = this.editQuestionForm.value;
      // ... your update logic ...

      this.isEditing = false;
    }
  }

  timespanToMinutes(timeSpan: string): number {
    const [hours, minutes, seconds] = timeSpan.split(':').map(Number);
    return hours * 60 + minutes + Math.floor(seconds / 60);
  }
}
