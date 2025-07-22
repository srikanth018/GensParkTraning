import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { VideoFileService } from '../../services/VideoFileService';

@Component({
  selector: 'app-upload-video',
  imports: [ReactiveFormsModule],
  templateUrl: './upload-video.html',
  styleUrl: './upload-video.css'
})
export class UploadVideo {
  @Output() videoUploaded: EventEmitter<boolean> = new EventEmitter();
  isUploading: boolean = false;
  uploadForm :FormGroup

  constructor(private videoFileService: VideoFileService) {
    this.uploadForm = new FormGroup({
      title: new FormControl(null,[]),
      description: new FormControl(null,[]),
      videoFile:new FormControl(null,[])
    })
  }

  onFileSelected(event: any) {
  const file: File = event.target.files[0];
  if (file) {
    this.uploadForm.patchValue({ videoFile: file });
  }
}


  uploadVideo() {
  this.isUploading = true;

  if (this.uploadForm.valid) {
    const formData = new FormData();
    formData.append('Title', this.uploadForm.get('title')?.value);
    formData.append('Description', this.uploadForm.get('description')?.value);

    const fileInput: File = this.uploadForm.get('videoFile')?.value;
    if (fileInput) {
      formData.append('VideoFile', fileInput);
    }

    this.videoFileService.uploadVideo(formData).subscribe({
      next: (response) => {
        console.log('Video uploaded successfully:', response);
        alert('Video uploaded successfully!');
        this.videoUploaded.emit(true);
        this.isUploading = false;
        this.uploadForm.reset();
      },
      error: (error) => {
        console.error('Error uploading video:', error);
        alert('Error uploading video: ' + error.message);
        this.isUploading = false;
      }
    });
  }
}

}
