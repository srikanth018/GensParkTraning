import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Loading } from './loading';
import { provideLottieOptions } from 'ngx-lottie';

describe('Loading', () => {
  let component: Loading;
  let fixture: ComponentFixture<Loading>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Loading],
      providers: [
        provideLottieOptions({
          player: () => import('lottie-web'),
        }),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(Loading);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
