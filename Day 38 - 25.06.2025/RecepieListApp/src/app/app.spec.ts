import { TestBed } from '@angular/core/testing';
import { App } from './app';
import { RecipeList } from './recipielist/recipielist';
import { RecipeService } from './services/recipe.service';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('App', () => {

  let recipeService: RecipeService;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App, RecipeList],
      providers: [RecipeService, provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();

    recipeService = TestBed.inject(RecipeService);
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Recipe List');
  });
});
