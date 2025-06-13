const API_URL = 'https://dummyjson.com/recipes';

function createRecipeCard(recipe) {
  return `
    <div class="card mb-4" style="width: 22rem;">
      <img src="${recipe.image}" class="card-img-top" alt="${recipe.name} "style="height: 200px; object-fit: cover; padding: 10px;">
      <div class="card-body">
        <h5 class="card-title">${recipe.name}</h5>
        <p class="card-text"><strong>Cuisine:</strong> ${recipe.cuisine}</p>
        <p class="card-text"><strong>Cooking Time:</strong> ${recipe.cookTimeMinutes} mins</p>
        <p class="card-text"><strong>Ingredients:</strong> ${recipe.ingredients.join(', ')}</p>
      </div>
    </div>
  `;
}

function showLoader() {
  document.getElementById('recipe-container').innerHTML = `
    <div class="d-flex justify-content-center my-5">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>
  `;
}

// Common render function
function renderRecipes(recipes) {
  const html = recipes.slice(0, 5).map(createRecipeCard).join('');
  document.getElementById('recipe-container').innerHTML = html;
}

function fetchRecipesCallback(callback) {
  fetch(API_URL)
    .then(response => response.json())
    .then(data => callback(null, data.recipes))
    .catch(error => callback(error, null));
}

function loadWithCallback() {
  showLoader();
  setTimeout(() => {
    fetchRecipesCallback((err, recipes) => {
      if (err) {
        document.getElementById('recipe-container').innerHTML = '<p class="text-danger">Error loading data.</p>';
      } else {
        renderRecipes(recipes);
      }
    });
  }, 2000);
}

function fetchRecipesPromise() {
  return fetch(API_URL).then(res => res.json()).then(data => data.recipes);
}

function loadWithPromise() {
  showLoader();
  setTimeout(() => {
    fetchRecipesPromise()
      .then(recipes => renderRecipes(recipes))
      .catch(() => {
        document.getElementById('recipe-container').innerHTML = '<p class="text-danger">Error loading data.</p>';
      });
  }, 2000);
}

async function loadWithAsyncAwait() {
  showLoader();
  await new Promise(resolve => setTimeout(resolve, 2000));
  try {
    const response = await fetch(API_URL);
    const data = await response.json();
    renderRecipes(data.recipes);
  } catch (error) {
    document.getElementById('recipe-container').innerHTML = '<p class="text-danger">Error loading data.</p>';
  }
}
