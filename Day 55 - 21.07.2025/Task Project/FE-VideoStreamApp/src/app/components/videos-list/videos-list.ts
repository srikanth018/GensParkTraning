import { Component } from '@angular/core';
import { VideoFileService } from '../../services/VideoFileService';
import { DatePipe, NgFor, NgIf } from '@angular/common';
import { UploadVideo } from '../upload-video/upload-video';

@Component({
  selector: 'app-videos-list',
  imports: [NgIf, NgFor, DatePipe, UploadVideo],
  templateUrl: './videos-list.html',
  styleUrl: './videos-list.css'
})
export class VideosList {
  isUploadDialogOpen: boolean = false;
  isVideoPlaying: boolean = false;
  playingVideo: any;
  videos: any[] = [];

  constructor(private videoFileService: VideoFileService) {
    this.loadVideos();
  }

  openUploadDialog()
  {
    this.isUploadDialogOpen = true;
    console.log('Opening upload dialog');
  }
  loadVideos() {
    this.videoFileService.getAllVideos().subscribe({
      next: (data) => {
        this.videos = data;
      },
      error: (err) => {
        console.error('Error loading videos', err);
      }
    });
  }
  playVideo(video: any) {
    this.playingVideo = video;
    this.isVideoPlaying = true;
    console.log('Playing video:', this.playingVideo);
  }
  closeVideo(){
    this.isVideoPlaying = false;
    this.playingVideo = null;
    console.log('Video closed');
  }
}
