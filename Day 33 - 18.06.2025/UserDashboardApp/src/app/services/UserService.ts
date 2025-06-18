
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable ()
export class UserService{
    private http = inject(HttpClient);

    getAllUsers(){
        return this.http.get('https://dummyjson.com/users');
    }
    getFilteredUsers(key:string, value:string){
        return this.http.get(`https://dummyjson.com/users/filter?key=${key}&value=${value}`)
    }
}

