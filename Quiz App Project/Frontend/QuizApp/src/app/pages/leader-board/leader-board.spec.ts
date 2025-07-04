import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaderBoard } from './leader-board';
import { QuizService } from '../../services/QuizService';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CompletedQuizService } from '../../services/CompletedQuizService';
import { AuthService } from '../../services/AuthService';
import { StudentService } from '../../services/StudentService';

describe('LeaderBoard', () => {
  let component: LeaderBoard;
  let fixture: ComponentFixture<LeaderBoard>;

  const mockJwt =
    'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.' +
    'eyJlbWFpbCI6InN0dWRlbnRAZXhhbXBsZS5jb20iLCJyb2xlIjoiU3R1ZGVudCJ9.' +
    'signature';

  beforeEach(async () => {

     spyOn(localStorage, 'getItem').and.callFake((key: string) => {
      if (key === 'access_token') {
        return mockJwt;
      }
      return null;
    });
    
    await TestBed.configureTestingModule({
      imports: [LeaderBoard],
      providers:[QuizService, provideHttpClient(), provideHttpClientTesting(), CompletedQuizService, AuthService, StudentService],
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaderBoard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

 

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
