import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserDetails } from './user-details';
import { UserModel } from '../../models/UserModel';
import { UserService } from '../../services/UserService';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

describe('UserDetails', () => {
  let component: UserDetails;
  let fixture: ComponentFixture<UserDetails>;
  let httpMock: HttpTestingController;
  let userService: UserService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserDetails],
      providers:[UserService, provideHttpClientTesting(), provideHttpClient()]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserDetails);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
    userService = TestBed.inject(UserService);
    fixture.detectChanges();
  });

   afterEach(() => {
    httpMock.verify(); // Ensure no pending HTTP requests
  });
  
  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize userData as an empty array', () => {
    expect(component.userData).toEqual([]);
  });
  const mockUser: UserModel = {
    id: 1,
    firstName: 'John',
    lastName: 'Doe',
    gender: 'male',
    role: 'admin',
    state: 'California'
  };
  it('should have a non-empty user data', () => {
    component.userData = [mockUser];
    expect(component.userData).toBeTruthy();
  });

  it('should filter admin role users', () => {
    component.filterUsers('role', 'admin');
    const req = httpMock.expectOne('https://dummyjson.com/users/filter?key=role&value=admin');
    expect(req.request.method).toBe('GET');
    // req.flush({ users: [mockUser] });
    // expect(component.userData).toEqual([mockUser]);
  });

  it('should filter users by role user', () => {
    component.filterUsers('role', 'user');
    const req = httpMock.expectOne('https://dummyjson.com/users/filter?key=role&value=user');
    expect(req.request.method).toBe('GET');
    req.flush({ users: [] });
    expect(component.userData).toEqual([]);
  });
});
