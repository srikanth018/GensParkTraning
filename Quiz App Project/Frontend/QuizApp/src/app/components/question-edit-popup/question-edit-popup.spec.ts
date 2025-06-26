import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionEditPopup } from './question-edit-popup';

describe('QuestionEditPopup', () => {
  let component: QuestionEditPopup;
  let fixture: ComponentFixture<QuestionEditPopup>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuestionEditPopup]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestionEditPopup);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
