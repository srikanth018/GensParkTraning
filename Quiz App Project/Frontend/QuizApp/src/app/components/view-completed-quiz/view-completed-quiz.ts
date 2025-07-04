import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CompletedQuizService } from '../../services/CompletedQuizService';
import { CompletedQuiz } from '../../models/CompletedQuiz';
import { QuizService } from '../../services/QuizService';
import { NgIf } from '@angular/common';
import { StudentService } from '../../services/StudentService';
  import html2canvas from 'html2canvas';
// import jsPDF from 'jspdf';
import { jsPDF } from 'jspdf';
import domtoimage from 'dom-to-image';
@Component({
  selector: 'app-view-completed-quiz',
  imports: [NgIf],
  templateUrl: './view-completed-quiz.html',
  styleUrl: './view-completed-quiz.css',
})
export class ViewCompletedQuiz implements OnInit {
  completedQuizId: string = '';
  completedQuiz: CompletedQuiz | null = null;
  constructor(
    private route: ActivatedRoute,
    private completedQuizService: CompletedQuizService,
    private quizService: QuizService,
    private studentService: StudentService, 
  ) {}
  ngOnInit() {
    this.completedQuizId = this.route.snapshot.paramMap.get('id') || '';
    this.loadCompletedQuiz();
  }

  loadCompletedQuiz() {
    this.completedQuizService
      .getCompletedQuizById(this.completedQuizId)
      .subscribe({
        next: (data) => {
          this.completedQuiz = data;
          this.getQuiz();
          this.getStudentEmail();
          console.log('Loaded completed quiz:', this.completedQuiz);
        },
        error: (error) => {
          console.error('Error loading completed quiz:', error);
        },
      });
  }

  getQuiz() {
    this.quizService.getQuizById(this.completedQuiz?.quizId ?? '').subscribe({
      next: (data) => {
        this.completedQuiz!.quizData = data;
        console.log('Loaded quiz:', this.completedQuiz?.quizData);
      },
      error: (error) => {
        console.error('Error loading quiz:', error);
      },
    });
  }

  getScorePercentage(completedQuiz: any): number {
    const total = completedQuiz?.quizData?.totalMarks ?? 0;
    const score = completedQuiz?.totalScore ?? 0;
    return total === 0 ? 0 : Math.round((score / total) * 100);
  }

  getTimePercentage(completedQuiz: any): number {
    const limit = this.parseTimeToSeconds(completedQuiz?.quizData?.timeLimit);
    const start = new Date(completedQuiz?.startedAt);
    const end = new Date(completedQuiz?.endedAt);
    const used = (end.getTime() - start.getTime()) / 1000;
    return limit === 0 ? 0 : Math.min(100, Math.round((used / limit) * 100));
  }

  getCompletedDuration(start: string, end: string): string {
    const startTime = new Date(start);
    const endTime = new Date(end);
    const diff = Math.floor((endTime.getTime() - startTime.getTime()) / 1000);

    const minutes = Math.floor(diff / 60);
    const seconds = diff % 60;
    return `${minutes}m ${seconds}s`;
  }

  parseTimeToSeconds(timeStr: string): number {
    const [hh, mm, ss] = timeStr.split(':').map(Number);
    return hh * 3600 + mm * 60 + ss;
  }

  student: any = null;
  getStudentEmail() {
    const email = this.completedQuiz?.studentEmail;
    if (email) {
      this.studentService.getStudentByEmail(email).subscribe({
        next: (data) => {
          this.student = data;
          console.log('Loaded student data:', this.student);
        },
        error: (error) => {
          console.error('Error loading student data:', error);
        },
      });
    }
  }



downloadPDF() {
  const element = document.getElementById('pdfContent');
  if (!element) return;

  domtoimage.toPng(element, { quality: 1, bgcolor: '#ffffff' })
    .then((dataUrl: string) => {
      const img = new Image();
      img.src = dataUrl;

      img.onload = () => {
        const pdf = new jsPDF('p', 'mm', 'a4');
        const pageWidth = pdf.internal.pageSize.getWidth();
        const pageHeight = pdf.internal.pageSize.getHeight();

        const pxToMm = 0.264583; // 1px = 0.264583 mm

        const imgWidthPx = img.width;
        const imgHeightPx = img.height;

        const imgWidthMm = imgWidthPx * pxToMm;
        const imgHeightMm = imgHeightPx * pxToMm;

        const ratio = pageWidth / imgWidthMm;
        const scaledHeight = imgHeightMm * ratio;

        let position = 0;

        if (scaledHeight <= pageHeight) {
          // Fits on one page
          pdf.addImage(img, 'PNG', 0, 0, pageWidth, scaledHeight);
        } else {
          // Multi-page logic
          let remainingHeight = scaledHeight;

          const canvas = document.createElement('canvas');
          const context = canvas.getContext('2d')!;
          canvas.width = img.width;
          canvas.height = img.height;

          context.drawImage(img, 0, 0);

          let pageCount = 0;
          const pageHeightPx = (pageHeight / ratio) / pxToMm;

          while (remainingHeight > 0) {
            const sourceY = pageCount * pageHeightPx;
            const sliceHeight = Math.min(pageHeightPx, img.height - sourceY);

            const sliceCanvas = document.createElement('canvas');
            const sliceContext = sliceCanvas.getContext('2d')!;
            sliceCanvas.width = img.width;
            sliceCanvas.height = sliceHeight;

            sliceContext.drawImage(
              canvas,
              0, sourceY, img.width, sliceHeight,
              0, 0, img.width, sliceHeight
            );

            const sliceDataUrl = sliceCanvas.toDataURL('image/png');
            const sliceHeightMm = sliceHeight * pxToMm * ratio;

            if (pageCount > 0) pdf.addPage();
            pdf.addImage(sliceDataUrl, 'PNG', 0, 0, pageWidth, sliceHeightMm);

            remainingHeight -= pageHeight;
            pageCount++;
          }
        }

        pdf.save(`${this.completedQuiz?.quizData?.title || 'quiz-summary'}.pdf`);
      };
    })
    .catch((error: any) => {
      console.error('PDF generation failed', error);
    });
}









}
