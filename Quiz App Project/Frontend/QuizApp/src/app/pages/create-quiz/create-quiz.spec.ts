import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateQuiz } from './create-quiz';
import { QuizService } from '../../services/QuizService';
import { AuthService } from '../../services/AuthService';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideMockStore } from '@ngrx/store/testing';

describe('CreateQuiz', () => {
  let component: CreateQuiz;
  let fixture: ComponentFixture<CreateQuiz>;

  const mockJwt =
    'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.' +
    'eyJlbWFpbCI6InRlYWNoZXJAZXhhbXBsZS5jb20iLCJyb2xlIjoiVGVhY2hlciJ9.' +
    'dummy-signature';

  beforeEach(() => {
    spyOn(localStorage, 'getItem').and.callFake((key: string) => {
      if (key === 'access_token') {
        return mockJwt;
      }
      return null;
    });
  });

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateQuiz],
      providers: [
        QuizService,
        AuthService,
        provideHttpClient(),
        provideHttpClientTesting(),
        provideMockStore({})
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CreateQuiz);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
