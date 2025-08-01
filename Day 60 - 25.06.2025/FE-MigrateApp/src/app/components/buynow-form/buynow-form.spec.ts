import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuynowForm } from './buynow-form';

describe('BuynowForm', () => {
  let component: BuynowForm;
  let fixture: ComponentFixture<BuynowForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BuynowForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BuynowForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
