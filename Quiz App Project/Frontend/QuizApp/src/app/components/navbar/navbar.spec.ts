import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Navbar } from './navbar';
import { AuthService } from '../../services/AuthService';
import { StudentService } from '../../services/StudentService';
import { TeacherService } from '../../services/TeacherService';
import { of } from 'rxjs';

describe('Navbar', () => {
  let component: Navbar;
  let fixture: ComponentFixture<Navbar>;

  beforeEach(async () => {
    localStorage.setItem(
      'access_token',
      // Sample payload: { nameid: "test@example.com", role: "Student" }
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.' +
        'eyJuYW1laWQiOiJ0ZXN0QGV4YW1wbGUuY29tIiwicm9sZSI6IlN0dWRlbnQifQ.' +
        'somesignature'
    );

    await TestBed.configureTestingModule({
      imports: [Navbar],
      providers: [
        {
          provide: AuthService,
          useValue: {
            decodeToken: (token: string) => ({
              nameid: 'test@gmail.com',
              role: 'Student',
            }),
          },
        },
        {
          provide: StudentService,
          useValue: {
            getStudentByEmail: () =>
              of({ name: 'Test Student' }), 
          },
        },
        {
          provide: TeacherService,
          useValue: {
            getTeacherByEmail: () =>
              of({ name: 'Test Teacher' }), // just in case
          },
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(Navbar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
    expect(component.username).toBe('Test Student');
  });
});
