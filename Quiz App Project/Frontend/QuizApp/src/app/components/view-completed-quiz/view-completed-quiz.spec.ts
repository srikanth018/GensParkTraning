import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCompletedQuiz } from './view-completed-quiz';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { QuizService } from '../../services/QuizService';
import { CompletedQuizService } from '../../services/CompletedQuizService';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { StudentService } from '../../services/StudentService';

describe('ViewCompletedQuiz', () => {
  let component: ViewCompletedQuiz;
  let fixture: ComponentFixture<ViewCompletedQuiz>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewCompletedQuiz],
      providers:[
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ id: 'sampleId' }),
            snapshot: {
              paramMap: {
                get: (key: string) => 'sampleId'
              },
              params: { id: 'sampleId' }
            }
          }
        },
        QuizService,
        CompletedQuizService,
        StudentService,
        provideHttpClient(),
        provideHttpClientTesting(),
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewCompletedQuiz);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
