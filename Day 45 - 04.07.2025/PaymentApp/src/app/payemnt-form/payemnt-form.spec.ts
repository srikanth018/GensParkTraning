import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PayemntForm } from './payemnt-form';
import { ReactiveFormsModule } from '@angular/forms';
import { NgZone } from '@angular/core';

describe('PayemntForm', () => {
  let component: PayemntForm;
  let fixture: ComponentFixture<PayemntForm>;
  let zone: NgZone;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, PayemntForm], // ✅ include standalone component
    }).compileComponents();

    fixture = TestBed.createComponent(PayemntForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize paymentForm with default values', () => {
    expect(component.paymentForm).toBeDefined();
    expect(component.paymentForm.valid).toBeFalse();
  });

  it('should validate form fields correctly', () => {
    const form = component.paymentForm;

    form.setValue({
      customerName: 'test',
      email: 'test@gmail.com',
      contactNumber: '9876543210',
      amount: 100,
    });

    expect(form.valid).toBeTrue();
  });

  it('should retrieve payment history from localStorage', () => {
    const mockHistory = [
      {
        customerName: 'Bob',
        email: 'bob@example.com',
        contactNumber: '9999999999',
        amount: 200,
        paymentId: 'pay_123',
        createdAt: new Date().toISOString(),
      },
    ];
    localStorage.setItem('paymentHistory', JSON.stringify(mockHistory));
    component.getPaymentHistory();

    expect(component.paymentHistory.length).toBe(1);
    expect(component.paymentHistory[0].customerName).toBe('Bob');
  });

it('should mock Razorpay openRazorpay and handle response', () => {
  spyOn(component, 'setItems');

  component.paymentForm.setValue({
    customerName: 'Test User',
    email: 'test@example.com',
    contactNumber: '9876543210',
    amount: 150,
  });

  const razorpayOpenSpy = jasmine.createSpy('open');
  const razorpayOnSpy = jasmine.createSpy('on');

  (window as any).Razorpay = function (options: any) {
    setTimeout(() => {
      options.handler({ razorpay_payment_id: 'mock_id_001' });
    }, 0);
    return {
      open: razorpayOpenSpy,
      on: razorpayOnSpy,
    };
  };

  component.openRazorpay();

  expect(razorpayOpenSpy).toHaveBeenCalled();
  expect(razorpayOnSpy).toHaveBeenCalledWith('payment.failed', jasmine.any(Function));
});

});
