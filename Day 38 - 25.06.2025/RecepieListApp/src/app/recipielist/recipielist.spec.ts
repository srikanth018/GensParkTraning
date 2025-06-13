import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { RecipeList } from './recipielist';
import { RecipeService } from '../services/recipe.service';
import { of, throwError } from 'rxjs';
import { RecipeModel } from '../models/recipe.model';

describe('RecipeList Component (Standalone)', () => {
  let component: RecipeList;
  let fixture: ComponentFixture<RecipeList>;
  let recipeServiceSpy: jasmine.SpyObj<RecipeService>;

  const dummyRecipes: RecipeModel[] = [
    new RecipeModel(1, 'Pasta', 'Italian', 30, ['noodles', 'sauce'], 'pasta.jpg'),
    new RecipeModel(2, 'Sushi', 'Japanese', 45, ['rice', 'fish'], 'sushi.jpg')
  ];

  beforeEach(async () => {
  const spy = jasmine.createSpyObj('RecipeService', ['getRecipes']);
  spy.getRecipes.and.returnValue(of({ recipes: [] })); 

  await TestBed.configureTestingModule({
    imports: [RecipeList], 
    providers: [{ provide: RecipeService, useValue: spy }],
  }).compileComponents();

  fixture = TestBed.createComponent(RecipeList);
  component = fixture.componentInstance;
  recipeServiceSpy = TestBed.inject(RecipeService) as jasmine.SpyObj<RecipeService>;
});


  it('should create the component', () => {
    recipeServiceSpy.getRecipes.and.returnValue(of({ recipes: [] }));
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should load recipes and update state', fakeAsync(() => {
  recipeServiceSpy.getRecipes.and.returnValue(of({ recipes: dummyRecipes }));

  fixture = TestBed.createComponent(RecipeList);
  component = fixture.componentInstance;
  fixture.detectChanges(); 

  tick(); 
  fixture.detectChanges(); 

  expect(component.recipes().length).toBe(2);
  expect(component.recipes()[0]).toEqual(dummyRecipes[0]);
  expect(component.recipes()[1]).toEqual(dummyRecipes[1]);
  expect(component.loading()).toBeFalse();
}));


it('should handle error and set loading to false', fakeAsync(() => {
  const consoleSpy = spyOn(console, 'error'); // <- move before fixture.createComponent

  recipeServiceSpy.getRecipes.and.returnValue(throwError(() => new Error('Failed to fetch')));

  fixture = TestBed.createComponent(RecipeList); // moved after spy setup
  component = fixture.componentInstance;
  fixture.detectChanges();
  tick();

  expect(consoleSpy).toHaveBeenCalledWith(jasmine.any(Error));
  expect(component.recipes()).toEqual([]);
  expect(component.loading()).toBeFalse();
}));

});
