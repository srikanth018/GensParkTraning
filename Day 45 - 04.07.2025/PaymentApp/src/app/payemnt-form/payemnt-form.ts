import { JsonPipe, NgFor, NgIf } from '@angular/common';
import { Component, NgZone, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
declare var Razorpay: any;

@Component({
  selector: 'app-payemnt-form',
  imports: [JsonPipe, NgIf, ReactiveFormsModule, NgFor],
  templateUrl: './payemnt-form.html',
  styleUrl: './payemnt-form.css',
})
export class PayemntForm implements OnInit {
  paymentForm: FormGroup;
  constructor(private zone: NgZone) {
    this.paymentForm = new FormGroup({
      customerName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      contactNumber: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[0-9]{10}$'),
      ]),
      amount: new FormControl(null, [Validators.required, Validators.min(1)]),
    });
  }

  ngOnInit() {
    this.getPaymentHistory();
  }

  paymentResponse: any;
  openRazorpay() {
    // if (this.paymentForm.invalid) {
    //   alert('Please fill all required fields correctly.');
    //   return;
    // }

    const { customerName, email, contactNumber, amount } =
      this.paymentForm.value;

    const options = {
      key: 'rzp_test_1DP5mmOlF5G5ag',
      amount: amount * 100,
      currency: 'INR',
      name: 'Payment App',
      description: 'Sample Payment',
      handler: (response: any) => {
        this.zone.run(() => {
          this.paymentResponse = response; 
          this.setItems();
        });
      },
      prefill: {
        name: customerName,
        contact: contactNumber,
        email: email,
      },
    };


    const rzp = new Razorpay(options);
    rzp.on('payment.failed', (response: any) => {
    this.zone.run(() => {
      alert('Payment Failed! Reason: ' + response.error.description);
    });
  });
    rzp.open();

    
  }

  setItems() {
    const { customerName, email, contactNumber, amount } =
      this.paymentForm.value;

    const newPayment = {
      customerName,
      email,
      contactNumber,
      amount,
      paymentId: this.paymentResponse?.razorpay_payment_id || 'unknown',
      createdAt: new Date().toISOString(),
    };

    const existingHistory = localStorage.getItem('paymentHistory');
    let history = existingHistory ? JSON.parse(existingHistory) : [];
    history.push(newPayment);
    localStorage.setItem('paymentHistory', JSON.stringify(history));
    this.paymentHistory = history;
    this.paymentForm.reset();
  }

  paymentHistory: any[] = [];
  getPaymentHistory() {
    const existingHistory = localStorage.getItem('paymentHistory');
    this.paymentHistory = existingHistory ? JSON.parse(existingHistory) : [];
  }
}
