import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { ProductService } from './services/productService';
import { provideHttpClient } from '@angular/common/http';
import { UserService } from './services/UserService';
import { AuthGuard } from './auth-guard';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    ProductService,
    provideHttpClient(),
    UserService,
    AuthGuard,
  ]
};
