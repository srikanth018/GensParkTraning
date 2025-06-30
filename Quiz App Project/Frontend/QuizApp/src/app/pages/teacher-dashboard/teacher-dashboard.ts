import { Component } from '@angular/core';
import { Toast } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { Ripple } from 'primeng/ripple';
import { MessageService } from 'primeng/api';
import { providePrimeNG } from 'primeng/config';
@Component({
  selector: 'app-teacher-dashboard',
  imports: [ButtonModule,Toast, Ripple],
  templateUrl: './teacher-dashboard.html',
  styleUrl: './teacher-dashboard.css',
})
export class TeacherDashboard {
constructor(private messageService: MessageService) {}

    showSuccess() {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Message Content' });
    }
}
