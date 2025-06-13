import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewQuizTeacher } from './view-quiz-teacher';

describe('ViewQuizTeacher', () => {
  let component: ViewQuizTeacher;
  let fixture: ComponentFixture<ViewQuizTeacher>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewQuizTeacher]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewQuizTeacher);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
