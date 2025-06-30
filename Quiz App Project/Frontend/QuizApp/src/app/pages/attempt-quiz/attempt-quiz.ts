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


declare var Toastify: any;

@Component({
  selector: 'app-attempt-quiz',
  imports: [FormsModule, NgFor, NgIf, ReactiveFormsModule, RouterOutlet, DisplayQuestions, Loading],
  templateUrl: './attempt-quiz.html',
  styleUrl: './attempt-quiz.css',
})
export class AttemptQuiz implements OnInit {
  exitCount: number = 0;
  message: string = ``;
  isFullScreen: boolean = false;
  isStarted: boolean = false;
  isCompleted: boolean = false;
  private quizService = inject(QuizService);
  quiz: any;

  constructor(
    private router: ActivatedRoute,
    private fb: FormBuilder,
    private route: Router
  ) {
    if (this.isFullScreen) {
      document.addEventListener('fullscreenchange', () => {
        this.exitCount++;
        this.message = `You've exited fullscreen ${this.exitCount} time${
          this.exitCount > 1 ? 's' : ''
        }. Stay focused!`;
        console.log('Fullscreen mode changed');
      });
    }
  }

  ngOnInit(): void {
    this.getQuizData();
    const controls = this.rules.map(
      () => new FormControl(false, Validators.requiredTrue)
    );
    this.agreementForm = this.fb.group(controls);
  }

  toggleFullScreen() {
    const elem = document.documentElement; // This makes the whole page go full screen

    if (document.fullscreenElement) {
      document.exitFullscreen();
    } else {
      this.isFullScreen = true;
      this.exitCount = 0;
      if (elem.requestFullscreen) {
        elem.requestFullscreen();
      } else if ((elem as any).webkitRequestFullscreen) {
        (elem as any).webkitRequestFullscreen(); // Safari compatibility
      }
    }
  }

  startQuiz() {
    
    if (this.agreementForm.valid) {
      console.log(this.agreementForm.value);

      console.log('Quiz Started');
      this.isStarted = true;
      this.toggleFullScreen();
      // this.route.navigate([
      //   'attempt-quiz',
      //   this.router.snapshot.paramMap.get('id'),
      //   'start-quiz',
      // ]);
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
    // 'If I exit Full Screen Mode more than 3 times, I understand that credit points will be reduced.',
    // 'I will not refresh or close the browser window until I submit the quiz.',
    // 'I understand the quiz will auto-submit when the timer ends.',
    // 'I will not open new tabs or switch to other applications during the quiz.',
  ];

  

  showToast() {
    Toastify({
      text: "This is a toast!",
      duration: 3000,
      gravity: "top",
      position: "right",
      close: true,
      backgroundColor: "linear-gradient(to right, #00b09b, #96c93d)",
      stopOnFocus: true
    }).showToast();
  }


showLoader: boolean = true;
response: any = null;
totalPossibleScore: number = 0;

handleQuizSubmit(submittedData: SubmitQuiz) {
this.totalPossibleScore = this.quiz?.totalMarks || 0;
  
  console.log('Quiz submitted: from attempt', submittedData);
  this.quizService.submitQuiz(submittedData).subscribe({
    next: (response) => {
      console.log('Quiz submitted successfully:', response);
      this.response = response;
      this.isStarted = false;
      this.isCompleted = true;
      
      // Show loader for 2 seconds before showing results
      setTimeout(() => {
        this.showLoader = false;
      }, 2000);
      
      if(document.fullscreenElement) {
        document.exitFullscreen();
      }
    },
    error: (error) => {
      console.error('Error submitting quiz:', error);
      this.showLoader = false;
    }
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

