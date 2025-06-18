import { Component, OnInit } from "@angular/core";
import { UserLoginModel } from "../models/UserLoginModel";
import { UserService } from "../services/UserService";
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Router, RouterOutlet } from "@angular/router";
import { passwordValidator } from "../misc/PasswordValidator";

@Component({
  selector: "app-login-page",
  imports: [FormsModule, RouterOutlet, ReactiveFormsModule],
  templateUrl: "./login-page.html",
  styleUrl: "./login-page.css",
})
export class LoginPage implements OnInit {
  userLoginData: UserLoginModel = new UserLoginModel();
  unError: boolean = false;
  pwError: boolean = false;

  unErrorMessage: string = "";
  passErrorMessage: string = "";

  loginForm: FormGroup;

  constructor(private userService: UserService, private router: Router) {
    this.loginForm = new FormGroup({
      un: new FormControl(null, [Validators.required, Validators.minLength(3)]),
      pass: new FormControl(null, [Validators.required, passwordValidator()]),
    });
  }

  public get un(): any {
    return this.loginForm.get("un");
  }
  public get pass(): any {
    return this.loginForm.get("pass");
  }

  // handleLogin(un:any, pw:any) {

  //   if(un.control.touched && un.control.errors){
  //     this.unErrorMessage=true;
  //   }

  //   if(pw.control.touched && pw.control.errors){
  //     this.pwErrorMessage=true;
  //   }

  //   if(pw.control.touched && un.control.touched) return;

  //   this.userService.validateUserLogin(this.userLoginData);
  //   this.router.navigate(['home', 'products']);

  // }

  ngOnInit(): void {
    this.un.valueChanges.subscribe(() => {
      this.checkUserNameErrors();
    });

    this.pass.valueChanges.subscribe(() => {
      this.checkPasswordErros();
    });
  }

  checkUserNameErrors() {

    if(this.un.valid){
      this.unError = false;
    }

    if (this.un.invalid) {
      if (this.un.errors?.required) {
        this.unErrorMessage = "User name cannot be empty";
      } else if (this.un.errors?.minlength) {
        this.unErrorMessage = "User name must contain at least 3 letters";
      }
      this.unError = true;
    }
  }

  checkPasswordErros() {
    if(this.pass.valid){
      this.pwError = false;
    }
    if (this.pass.invalid) {
      if (this.pass.errors?.required) {
        this.passErrorMessage = "Password cannot be empty";
      } else if (this.pass.errors?.lengthError) {
        this.passErrorMessage = this.pass.errors?.lengthError;
      }
      this.pwError = true;
    }
  }

  handleLogin() {

    this.loginForm.markAllAsTouched();

    this.checkUserNameErrors();
    this.checkPasswordErros();

    // this.userService.validateUserLogin(this.userLoginData);
    // this.router.navigate(['home', 'products']);
  }
}
