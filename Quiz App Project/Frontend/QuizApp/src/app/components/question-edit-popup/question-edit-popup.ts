import { NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-question-edit-popup',
  imports: [ReactiveFormsModule, NgFor, NgIf],
  templateUrl: './question-edit-popup.html',
  styleUrl: './question-edit-popup.css'
})
export class QuestionEditPopup {
  @Input() questionform!: FormGroup;
@Output() save = new EventEmitter<void>();
@Output() cancel = new EventEmitter<void>();

onSubmit() {
  if (this.questionform.valid) {
    this.save.emit();
  }
}

onCancel() {
  this.cancel.emit();
}

// Uncomment if you need options functionality
get options() {
  return this.questionform.get('options') as FormArray;
}

}
