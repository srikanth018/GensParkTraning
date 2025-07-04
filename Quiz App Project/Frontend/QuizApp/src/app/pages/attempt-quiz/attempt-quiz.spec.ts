import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttemptQuiz } from './attempt-quiz';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { QuizService } from '../../services/QuizService';
import { CompletedQuizService } from '../../services/CompletedQuizService';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ToastrModule } from 'ngx-toastr';

describe('AttemptQuiz', () => {
  let component: AttemptQuiz;
  let fixture: ComponentFixture<AttemptQuiz>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AttemptQuiz, ToastrModule.forRoot()],
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
        
        provideHttpClient(),
        provideHttpClientTesting(),
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttemptQuiz);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
