import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { ProductService } from './services/ProductService';
import { provideHttpClient } from '@angular/common/http';
import { CartService } from './services/CartService';
import { OrderService } from './services/OrderService';
import { NewsService } from './services/NewsService';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    ProductService,
    CartService,
    OrderService,
    NewsService,
    provideHttpClient()
  ]
};
