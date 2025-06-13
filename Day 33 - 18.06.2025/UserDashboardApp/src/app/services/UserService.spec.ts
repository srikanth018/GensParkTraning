import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { UserService } from './UserService';
import { provideHttpClient } from '@angular/common/http';

describe('UserService', () => {
  let service: UserService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserService, provideHttpClient(), provideHttpClientTesting()],
      imports: [],
    });
    service = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });
  afterEach(() => {
    httpMock.verify();
  });
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch all users', (done) => {
    service.getAllUsers().subscribe((users: any) => {
      expect(users.total).toBe(208);
      done();
    });
    const mockResponse = { total: 208, users: [{ id: 1, name: 'John Doe' }] };
    const req = httpMock.expectOne('https://dummyjson.com/users'); 
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });
});
