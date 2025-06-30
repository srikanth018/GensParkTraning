import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewQuizStudent } from './view-quiz-student';

describe('ViewQuizStudent', () => {
  let component: ViewQuizStudent;
  let fixture: ComponentFixture<ViewQuizStudent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewQuizStudent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewQuizStudent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
