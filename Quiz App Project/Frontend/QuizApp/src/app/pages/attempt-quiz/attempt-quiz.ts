import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { QuizService } from '../../services/QuizService';
import { QuizResponseMapper } from '../../misc/QuizResponseMapper';
// import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { DisplayQuestions } from '../../components/display-questions/display-questions';
import { SubmitQuiz } from '../../models/SubmitQuiz';
import { Loading } from '../../components/loading/loading';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-attempt-quiz',
  imports: [
    FormsModule,
    NgFor,
    NgIf,
    ReactiveFormsModule,
    RouterOutlet,
    DisplayQuestions,
    Loading,
  ],
  templateUrl: './attempt-quiz.html',
  styleUrl: './attempt-quiz.css',
})
export class AttemptQuiz implements OnInit {
  exitCount: number = 0;
  message: string = ``;
  isFullScreen: boolean = false;
  isStarted: boolean = false;
  isCompleted: boolean = false;
  negativePoints: number = 0;
  private quizService = inject(QuizService);
  quiz: any;

  constructor(
    private router: ActivatedRoute,
    private fb: FormBuilder,
    private route: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getQuizData();
    const controls = this.rules.map(
      () => new FormControl(false, Validators.requiredTrue)
    );
    this.agreementForm = this.fb.group(controls);

    document.addEventListener('fullscreenchange', () => {
      const isNowFullScreen = !!document.fullscreenElement;

      if (!isNowFullScreen && this.isFullScreen) {
        this.exitCount++;
        this.message = `You've exited fullscreen ${this.exitCount} time${
          this.exitCount > 1 ? 's' : ''
        }. Stay focused!`;
        console.warn(this.message);
        this.toastr.warning(this.message, 'Fullscreen Warning', {
          timeOut: 3000,
          positionClass: 'toast-top-right',
        });
        this.enterFullScreen();
        if (this.isStarted && !this.isCompleted) {
          this.enterFullScreen();

          if (this.exitCount >= 3) {
            this.negativePoints += 1;
            console.warn(
              `You have exited fullscreen ${this.exitCount} times. 1 point will be deducted from your Credit Points.`
            );
            this.toastr.error(
              `You have exited fullscreen ${this.exitCount} times. 1 point will be deducted from your Credit Points.`,
              'Negative Points',
              {
                timeOut: 3000,
                positionClass: 'toast-top-center',
              }
            );
          }
        }
      }

      this.isFullScreen = isNowFullScreen;
    });
  }

  enterFullScreen() {
    console.log('Entering Full Screen Mode');
    
    const elem = document.documentElement;

    if (elem.requestFullscreen) {
      elem.requestFullscreen();
    } else if ((elem as any).webkitRequestFullscreen) {
      (elem as any).webkitRequestFullscreen(); 
    }

    this.isFullScreen = true;
  }

  toggleFullScreen() {
    if (document.fullscreenElement) {
      document.exitFullscreen();
    } else {
      this.enterFullScreen();
    }
  }

  startQuiz() {
    if (this.agreementForm.valid) {
      console.log('Quiz Started');
      this.toastr.info('Quiz Started', 'Information', {
        timeOut: 2000,
        positionClass: 'toast-top-right',
      });
      this.isStarted = true;
      this.exitCount = 0;
      this.enterFullScreen();
    } else {
      this.isStarted = false;
      console.log('Please accept all rules before starting.');
    }
  }

  getQuizData() {
    const id = this.router.snapshot.paramMap.get('id');
    if (id) {
      this.quizService.getQuizById(id).subscribe({
        next: (data: any) => {
          this.quiz = QuizResponseMapper.mapResponseToQuiz(data);

          console.log(this.quiz);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  agreementForm!: FormGroup;
  rules: string[] = [
    'I agree to enable Full Screen Mode while attempting the quiz.',
    'If I exit Full Screen Mode more than 3 times, I understand that credit points will be reduced.',
    'I will not refresh or close the browser window until I submit the quiz.',
    'I understand the quiz will auto-submit when the timer ends.',
  ];

  showLoader: boolean = true;
  response: any = null;
  totalPossibleScore: number = 0;

  handleQuizSubmit(submittedData: SubmitQuiz) {
    this.totalPossibleScore = this.quiz?.totalMarks || 0;

    console.log('Quiz submitted: from attempt', submittedData);
    this.quizService.submitQuiz(submittedData).subscribe({
      next: (response) => {
        console.log('Quiz submitted successfully:', response);
        this.toastr.success('Quiz submitted successfully!', 'Success', {
          timeOut: 3000,
          positionClass: 'toast-top-right',
        });
        this.response = response;
        this.isStarted = false;
        this.isCompleted = true;

        // Show loader for 2 seconds before showing results
        setTimeout(() => {
          this.showLoader = false;
        }, 2000);

        if (document.fullscreenElement) {
          document.exitFullscreen();
        }
      },
      error: (error) => {
        console.error('Error submitting quiz:', error);
        this.showLoader = false;
      },
    });
  }

  // Add this method for navigation
  backToQuizzes() {
    this.route.navigate(['/main/available-quizzes']);
  }

  getDurationInMinutes(start: string, end: string): number {
    if (!start || !end) return 0;

    const startTime = new Date(start).getTime();
    const endTime = new Date(end).getTime();
    const diffInMs = endTime - startTime;
    return Math.round(diffInMs / (1000 * 60)); // convert to minutes
  }
}
