import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Recipielist } from './recipielist';

describe('Recipielist', () => {
  let component: Recipielist;
  let fixture: ComponentFixture<Recipielist>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Recipielist]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Recipielist);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
