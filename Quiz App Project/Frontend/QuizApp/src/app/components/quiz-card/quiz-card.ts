import { Component, Input } from '@angular/core';
import { Quiz } from '../../models/QuizModel';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-quiz-card',
  standalone: true,
  imports: [NgIf],
  templateUrl: './quiz-card.html',
  styleUrl: './quiz-card.css'
})
export class QuizCard {
  @Input() quiz!: Quiz;
  @Input() type: string = 'list';

  showFullDescription: boolean = false;
}
