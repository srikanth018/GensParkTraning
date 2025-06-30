import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCompletedQuiz } from './view-completed-quiz';

describe('ViewCompletedQuiz', () => {
  let component: ViewCompletedQuiz;
  let fixture: ComponentFixture<ViewCompletedQuiz>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewCompletedQuiz]
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
