import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable } from 'rxjs';

@Injectable()
export class QuizService {
  constructor(private http: HttpClient) {}

  private baseUrl = 'http://localhost:5038/api/v1/';

  createQuiz(quizData: any): Observable<string> {
    const token = localStorage.getItem('access_token');
    const headers = {
      Authorization: `Bearer ${token}`,
    };
    return this.http
      .post(`${this.baseUrl}quizzes/quiz`, quizData, {
        observe: 'response',
        headers,
      })
      .pipe(
        map((response) =>
          response.status >= 200 && response.status < 300
            ? 'Quiz created successfully'
            : 'Failed to create quiz'
        ),
        catchError((error) => {
          console.error('Error creating quiz:', error);
          return 'Error creating quiz';
        })
      );
  }

  downloadQuizTemplate(
    questionCount: number = 5,
    optionCount: number = 4
  ): Observable<Blob> {
    const token = localStorage.getItem('access_token');
    const headers = {
      Authorization: `Bearer ${token}`,
    };
    return this.http.get('http://localhost:5038/api/v1/quizzes/template', {
      params: new HttpParams()
        .set('questionCount', questionCount)
        .set('optionCount', optionCount),
      headers,
      responseType: 'blob',
    });
  }

  bulkUploadQuiz(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file); // ⚠️ Name 'file' must match parameter [FromForm] FileUploadDTO file.File

    const token = localStorage.getItem('access_token');
    const headers = {
      Authorization: `Bearer ${token}`,
    };
    return this.http.post(
      'http://localhost:5038/api/v1/quizzes/bulk-upload',
      formData,
      { headers }
    );
  }

  getUploadedQuizzes(teacherEmail: string): Observable<any> {
    const token = localStorage.getItem('access_token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http
      .get(`${this.baseUrl}quizzes/getbyteacher?email=${teacherEmail}`, { headers })
      .pipe(
        map((response) => response),
        catchError((error) => {
          console.error('Error fetching uploaded quizzes:', error);
          throw error;
        })
      );
  }

  getQuizById(quizId: string): Observable<any> {
    const token = localStorage.getItem('access_token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http
      .get(`${this.baseUrl}quizzes/${quizId}`, { headers })
      .pipe(
        map((response) => response),
        catchError((error) => {
          console.error('Error fetching quiz:', error);
          throw error;
        })
      );
  }

  
}
