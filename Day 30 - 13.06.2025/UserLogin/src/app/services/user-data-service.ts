import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserModel } from '../models/usermodel';

@Injectable({
  providedIn: 'root',
})
export class UserDataService {


  private http = inject(HttpClient);
  private apiUrl = 'https://dummyjson.com/users';

  getUsers() {
    return this.http.get<any>(this.apiUrl);
  }
  getUserByEmail(email: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/search?q=${email}`);
  }


  private usernameSubject = new BehaviorSubject<string|null>(null);
  username$:Observable<string|null> = this.usernameSubject.asObservable();

 validateUser (user:UserModel){
    if(user.username.length<3){
      this.usernameSubject.error("username should be more than 3 characters");
    }else{
      
      this.usernameSubject.next(user.username);
      
    }
  }
}

