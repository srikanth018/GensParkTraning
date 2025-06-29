import { Component, inject, Input, OnInit } from '@angular/core';
import {
  FormArray,
  FormControl,
  FormGroup,
  Validators,
  FormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { createQuizFormValidators } from '../../misc/createQuizFormValidators';
import { Store } from '@ngrx/store';
import { selectUser } from '../../ngrx/authStore/auth.selector';
import { map, Subscription } from 'rxjs';
import { Question } from '../../components/question/question';
import { NgFor, NgIf } from '@angular/common';
import { QuizService } from '../../services/QuizService';
import { AuthService } from '../../services/AuthService';
import { Loading } from "../../components/loading/loading";


@Component({
  selector: 'app-create-quiz',
  imports: [Question, NgFor, NgIf, ReactiveFormsModule, Loading],
  templateUrl: './create-quiz.html',
  styleUrl: './create-quiz.css',
  standalone: true,
})
export class CreateQuiz implements OnInit {

  private authService = inject(AuthService);
  private formSubscription: Subscription | undefined;

  private store = inject(Store);
  quizForm: FormGroup;
  isloading: boolean = false;

  constructor(public fb: FormBuilder, private quizService: QuizService) {
    this.quizForm = new FormGroup({
      title: new FormControl(null, [
        Validators.required,
        createQuizFormValidators.titleValidator(),
      ]),
      description: new FormControl(null, [
        Validators.required,
        createQuizFormValidators.descriptionValidator(),
      ]),
      category: new FormControl(null, [Validators.required]),
      uploadedBy: new FormControl(
        { value: this.getTeacherEmail(), disabled: true },
        [Validators.required]
      ),
      totalMarks: new FormControl(null, [
        Validators.required,
        createQuizFormValidators.totalMarksValidator(),
      ]),
      questions: new FormArray([]),
    });
  }

  get questions(): FormArray {
    return this.quizForm.get('questions') as FormArray;
  }

  addQuestion(): void {
    const questionGroup = this.fb.group({
      questionText: new FormControl(null, [
        Validators.required,
        Validators.minLength(10),
      ]),
      mark: new FormControl(1, [Validators.required, Validators.min(1)]),
      image: null,
      options: this.fb.array([]),
    });

    this.questions.push(questionGroup);
  }

  removeQuestion(i: number): void {
    this.questions.removeAt(i);
  }

  teacherEmail: string | null = '';
  getTeacherEmail(): string | null {
    const user = this.authService.decodeToken(
      localStorage.getItem('access_token') || ''
    );
    this.teacherEmail = user?.nameid ?? null;
    return this.teacherEmail;
  }

  getQuestionGroup(index: number): FormGroup {
    return this.questions.at(index) as FormGroup;
  }

  ngOnInit() {
    this.loadFormFromLocalStorage();
    this.formSubscription = this.quizForm.valueChanges.subscribe(() => {
      localStorage.setItem(
        'createQuizForm',
        JSON.stringify(this.quizForm.getRawValue())
      );
    });
  }

  submitQuiz() {
    if (this.quizForm.valid) {
      this.quizService.createQuiz(this.quizForm.value).subscribe({
        next: (response) => {
          console.log('Quiz created successfully:', response);
          this.formSubscription?.unsubscribe();
          localStorage.removeItem('createQuizForm');
          this.quizForm.reset();
        },
        error: (error) => {
          console.error('Error creating quiz:', error);
        },
      });
      console.log('Quiz submitted:', this.quizForm.value);
    } else {
      console.log('Form is invalid');
    }
  }

  loadFormFromLocalStorage() {
    const saved = localStorage.getItem('createQuizForm');
    if (saved) {
      const parsed = JSON.parse(saved);

      this.quizForm.patchValue({
        title: parsed.title,
        category: parsed.category,
        uploadedBy: parsed.uploadedBy || this.getTeacherEmail(),
        totalMarks: parsed.totalMarks,
        description: parsed.description,
      });

      const questionsArray = this.quizForm.get('questions') as FormArray;
      // questionsArray.clear();

      parsed.questions.forEach((q: any) => {
        const questionGroup = this.fb.group({
          questionText: [q.questionText],
          mark: [q.mark],
          image: null,
          options: this.fb.array([]),
        });

        q.options.forEach((o: any) => {
          (questionGroup.get('options') as FormArray).push(
            this.fb.group({
              optionText: [o.optionText],
              isCorrect: [o.isCorrect],
            })
          );
        });

        questionsArray.push(questionGroup);
      });
    }
  }

  downloadTemplate() {
    this.isloading=true;
    this.quizService.downloadQuizTemplate(5, 4).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'QuizTemplate.xlsx'; // file name
        a.click();
        window.URL.revokeObjectURL(url);
        this.isloading=false;
      },
      error: (err) => {
        console.error('Error downloading file:', err);
        this.isloading=false;
      },
    });
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.quizService.bulkUploadQuiz(file).subscribe({
        next: (res) => {
          console.log('Bulk upload success:', res);
          alert(`Quiz "${res.title}" uploaded successfully!`);
        },
        error: (err) => {
          console.error('Error uploading file:', err);
          alert('Error uploading quiz.');
        },
      });
    }
  }

  clearform() {
    this.quizForm.reset();
    this.questions.clear();
    localStorage.removeItem('createQuizForm');
    this.formSubscription?.unsubscribe();
  }
}
