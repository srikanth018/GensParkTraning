import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayQuestions } from './display-questions';

describe('DisplayQuestions', () => {
  let component: DisplayQuestions;
  let fixture: ComponentFixture<DisplayQuestions>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayQuestions]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DisplayQuestions);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
