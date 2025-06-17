import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { UserLoginModel } from "../models/UserLoginModel";

@Injectable()

export class UserService{
    private http = inject(HttpClient);

    private usernameSubject = new BehaviorSubject<string|null>(null);
    username$:Observable<string|null> = this.usernameSubject.asObservable();
    
    validateUserLogin(user:UserLoginModel):boolean
    {
        if(user.username.length<3)
        {
            this.usernameSubject.next(null);
            return false;
        }
            
        else
        {
            this.callLoginAPI(user).subscribe(
                {
                    next:(data:any)=>{
                            console.log(`from service ${user.username},${user.password}`)

                        this.usernameSubject.next(user.username);
                        localStorage.setItem("token",data.accessToken)
                        return true;
                    }
                }
            )
            
        }
        return true;
            
    }
    callGetProfile()
    {
        var token = localStorage.getItem("token")
        const httpHeader = new HttpHeaders({
            'Authorization':`Bearer ${token}`
        })
        return this.http.get('https://dummyjson.com/auth/me',{headers:httpHeader});
        
    }

    callLoginAPI(user:UserLoginModel)
    {
        console.log("from login api")
        return this.http.post("https://dummyjson.com/auth/login",user);
    }
    logout(){
        this.usernameSubject.next(null);
    }
}