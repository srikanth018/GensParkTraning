import { TestBed } from '@angular/core/testing';
import { RecipeService } from './recipe.service';
import {
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

describe('RecipeService', () => {
  let service: RecipeService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        RecipeService,
        provideHttpClient(),
        provideHttpClientTesting(),
      ],
    });

    service = TestBed.inject(RecipeService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Ensure no pending requests
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  it('should fetch recipes from API', () => {
    const mockRecipes = {
      recipes: [
        {
          id: 1,
          name: 'Pasta',
          cuisine: 'Italian',
          cookTimeMinutes: 30,
          ingredients: ['noodles', 'sauce'],
          image: 'pasta.jpg',
        },
        {
          id: 2,
          name: 'Sushi',
          cuisine: 'Japanese',
          cookTimeMinutes: 45,
          ingredients: ['rice', 'fish'],
          image: 'sushi.jpg',
        },
      ],
    };

    service.getRecipes().subscribe((res) => {
      expect(res).toEqual(mockRecipes);
    });

    const req = httpMock.expectOne('https://dummyjson.com/recipes');
    expect(req.request.method).toBe('GET');
    req.flush(mockRecipes);
  });
});
