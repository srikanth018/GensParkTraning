import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadedQuizzes } from './uploaded-quizzes';

describe('UploadedQuizzes', () => {
  let component: UploadedQuizzes;
  let fixture: ComponentFixture<UploadedQuizzes>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UploadedQuizzes]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UploadedQuizzes);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
