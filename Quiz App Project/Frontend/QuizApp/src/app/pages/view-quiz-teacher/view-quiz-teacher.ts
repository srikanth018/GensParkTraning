import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/AuthService';
import { QuizService } from '../../services/QuizService';

@Component({
  selector: 'app-view-quiz-teacher',
  imports: [],
  templateUrl: './view-quiz-teacher.html',
  styleUrl: './view-quiz-teacher.css'
})
export class ViewQuizTeacher implements OnInit {
  
  isloading:boolean=false;
  quizId:string='';
  quiz:any = [];
  constructor(private route:ActivatedRoute, private quizService:QuizService){}
  ngOnInit(): void {
    this.quizId = this.route.snapshot.params['id'];
    this.getQuizData();

  }

  getQuizData(){
    this.isloading=true;
    this.quizService.getQuizById(this.quizId).subscribe({
      next:(data)=>{
        this.quiz = data;
        this.isloading = false;
        console.log(this.quiz);
      },
      error:(err)=>{
        console.log(err);
        this.isloading = false;
      }
    });
  }
}
