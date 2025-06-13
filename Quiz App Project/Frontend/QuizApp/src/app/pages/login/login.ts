import { Component, inject } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { AuthService } from '../../services/AuthService';
import { Store } from '@ngrx/store';
import * as AuthActions from '../../ngrx/authStore/auth.action';
import {
  selectError,
  selectIsAuthenticated,
  selectUser,
  selectLoading,
} from '../../ngrx/authStore/auth.selector';
import { AuthState } from '../../ngrx/authStore/AuthState';
import { AsyncPipe, NgIf } from '@angular/common';
import { Register } from '../register/register';
import { Loading } from '../../components/loading/loading';
import { Router } from '@angular/router';
import { Success } from '../../components/success/success';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, NgIf, Register, Loading, AsyncPipe, Success],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  isloading: boolean = false;
  errorMessage: string = '';
  showSuccess: boolean = false;
  loginForm: FormGroup;
  successMessage:string | null='';
  RegMessage: string | null = localStorage.getItem('reg');

  private store = inject(Store);
  private router = inject(Router);
  constructor(private authService: AuthService) {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
    });
  }

  loggedInUserData: any;
  ngOnInit() {
    const token = localStorage.getItem('access_token');
    if (token) {
      const user = this.authService.decodeToken(token);
      this.store.dispatch(AuthActions.loginSuccess({ token, user }));
    }
    this.store.select(selectUser).subscribe((user) => {
      this.loggedInUserData = user;
    });
  }

  handleRegistrationSuccess(message: string) {
  this.successMessage = message;
  this.showSuccess = true;

  setTimeout(() => {
    this.toggleForm();
    this.showSuccess = false;
    this.successMessage = null;
    this.RegMessage = null;
    localStorage.removeItem('reg');
  }, 3000);
}

  isloading$ = this.store.select(selectLoading);

  async login() {
    if (this.loginForm.invalid) return;
    this.store.dispatch(
      AuthActions.login({
        email: this.loginForm.value.email,
        password: this.loginForm.value.password,
      })
    );
    this.store.select(selectUser).subscribe((user) => {
      this.loggedInUserData = user;
      this.store.select(selectIsAuthenticated).subscribe((isAuthenticated) => {
        console.log('Is Authenticated:', isAuthenticated);
        console.log('Logged In User Data:', this.loggedInUserData);
        if (isAuthenticated) {
          this.successMessage = 'Login Successful!!!'
          this.showSuccess = true;
          setTimeout(() => {
            this.router.navigate(['create-quiz']);
          }, 3000);
        }
      });
    });
    this.store.select(selectError).subscribe((error) => {
      if (error) {
        // alert(error);
        console.error('Login Error:', error);
        this.errorMessage = error;
      }
    });
  }

  showLogin = true;

  toggleForm() {
    this.showLogin = !this.showLogin;
  }
}
