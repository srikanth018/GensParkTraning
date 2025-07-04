import { ComponentFixture, TestBed } from '@angular/core/testing';
import { QuizHistory } from './quiz-history';
import { QuizService } from '../../services/QuizService';
import { AuthService } from '../../services/AuthService';
import { CompletedQuizService } from '../../services/CompletedQuizService';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('QuizHistory', () => {
  let component: QuizHistory;
  let fixture: ComponentFixture<QuizHistory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuizHistory],
      providers: [
        QuizService,
        AuthService,
        CompletedQuizService,
        provideHttpClient(),
        provideHttpClientTesting(),
      ],
    }).compileComponents();

    spyOn(localStorage, 'getItem').and.callFake((key: string) => {
      if (key === 'access_token') {
        return 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJ0ZXN0QGVtYWlsLmNvbSIsInJvbGUiOiJTdHVkZW50In0.signature';
      }
      return null;
    });

    fixture = TestBed.createComponent(QuizHistory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
