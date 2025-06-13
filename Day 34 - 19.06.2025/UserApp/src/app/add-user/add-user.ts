import { Component, NgModule, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { User } from '../models/User';
import { addUSer, loadUsers } from '../ngrx/user.action';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { customFromValidation } from '../misc/customFormValidations';
import { NgIf } from '@angular/common';
import { selectAllUsers } from '../ngrx/user.selector';

@Component({
  selector: 'app-add-user',
  imports: [NgIf, ReactiveFormsModule],
  templateUrl: './add-user.html',
  styleUrl: './add-user.css',
})
export class AddUser implements OnInit {
  constructor(private store: Store) {
    this.store.select(selectAllUsers).subscribe((users) => {
      this.usersData = users;
    });
  }

  password: string = '';
  confirmPassword: string = '';

  usersData: User[] = [];
  user: User = new User();
  handleAddUser() {
    this.user = this.mapFormToUser();
    this.store.dispatch(addUSer({ user: this.user }));
  }

  addUserForm: FormGroup | undefined;
  ngOnInit(): void {
    this.store.dispatch(loadUsers());
    this.addUserFormControls();
  }

  addUserFormControls(){
  this.addUserForm = new FormGroup({
    userName: new FormControl(null, [customFromValidation.UsernameValidator()]),
    email: new FormControl(null, [Validators.email]),
    role: new FormControl(null, []),
    password: new FormControl(null, [customFromValidation.PasswordValidator()]),
    confirmPassword: new FormControl(null, [])
  }, { validators: customFromValidation.ConfirmPassword() });
}

  mapFormToUser(): User {
    return new User(
      this.usersData.length + 1,
      this.addUserForm?.get('userName')?.value,
      this.addUserForm?.get('email')?.value,
      this.addUserForm?.get('role')?.value,
      this.addUserForm?.get('password')?.value
    );
  }

  startCamera() {
  navigator.mediaDevices.getUserMedia({ video: true })
    .then((stream) => {
      const videoElement = document.querySelector('video');
      if (videoElement) {
        videoElement.srcObject = stream;
      }
    })
    .catch((err) => {
      console.error("Camera permission denied:", err);
    });
}

}
