import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()

export class VideoFileService{
    private http = inject(HttpClient);
    private baseurl = "http://localhost:5047/api/VideoStream";

    getAllVideos() : Observable<any[]> {
        return this.http.get<any[]>(`${this.baseurl}`);
    }
    uploadVideo(videoData: FormData): Observable<any> {
        return this.http.post<any>(`${this.baseurl}`, videoData);
    }
}
