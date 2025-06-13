import { Component, EventEmitter, inject, Output } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { registerFormValidators } from '../../misc/registerFormValidators';
import { RegisterStudentModel } from '../../models/RegisterStudentModel';
import { RegisterTeacherModel } from '../../models/RegisterTeacherModel';
import { RegisterService } from '../../services/RegisterService';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  @Output() registrationSuccess = new EventEmitter<string>();
  registerForm: FormGroup;

  selectedRole: string = '';

  userData: any;

  private registerService = inject(RegisterService);

  constructor() {
    this.registerForm = new FormGroup(
      {
        name: new FormControl(null, [
          Validators.required,
          registerFormValidators.nameValidator(),
        ]),
        email: new FormControl(null, [Validators.required, Validators.email]),
        phoneNumber: new FormControl(null, [
          Validators.required,
          Validators.pattern(/^\d{10}$/),
        ]),
        highestQualification: new FormControl(null, [Validators.required]),
        dateOfBirth: new FormControl(null, [Validators.required]),
        password: new FormControl(null, [
          Validators.required,
          registerFormValidators.PasswordValidator(),
        ]),
        confirmPassword: new FormControl(null, [Validators.required]),
      },
      { validators: registerFormValidators.ConfirmPassword() }
    );

    this.registerForm.get('confirmPassword')?.valueChanges.subscribe(() => {
      this.registerForm.updateValueAndValidity();
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      if (this.selectedRole === 'Student') {
        this.userData = RegisterStudentModel.mapStudentModel(
          this.registerForm.value
        );
        this.registerService.registerStudent(this.userData).subscribe({
          next: (response) => {
            const successMsg = 'Registration Successful !!!';
            this.registrationSuccess.emit(successMsg);
            console.log('Student registered successfully:', response);
          },
          error: (error) => {
            console.error('Error registering student:', error.error.message);
          },
        });
      }
      if (this.selectedRole === 'Teacher') {
        this.userData = RegisterTeacherModel.mapTeacherModel(
          this.registerForm.value
        );
        this.registerService.registerTeacher(this.userData).subscribe({
          next: (response) => {
            localStorage.setItem('reg', 'Registration Successfull !!!!');

            console.log('Teacher registered successfully:', response);
          },
          error: (error) => {
            console.error('Error registering teacher:', error.message);
          },
        });
      }
    } else {
      console.log('Form is invalid');
    }

    // const successMsg = 'Registration Successful !!!';
    // this.registrationSuccess.emit(successMsg);
  }
}
