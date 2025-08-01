import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UpdateNewsModel } from "../Models/UpdateNewsModel";
import { CreateNewsModel } from "../Models/CreateNewsModel";

@Injectable({
  providedIn: 'root'
})
export class NewsService {
    private http = inject(HttpClient);

    private baseUrl = 'http://localhost:5041/api/News/';

    getNews(){
        return this.http.get(`${this.baseUrl}`);
    }
    updateNews(id: number, newsData: UpdateNewsModel) {
        return this.http.put(`${this.baseUrl}${id}`, newsData);
    }
    createNews(newsData: CreateNewsModel) {
        return this.http.post(`${this.baseUrl}`, newsData);
    }
    deleteNews(id: number) {
        return this.http.delete(`${this.baseUrl}${id}`);
    }
}

