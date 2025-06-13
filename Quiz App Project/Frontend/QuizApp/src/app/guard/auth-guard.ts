import { inject, Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  GuardResult,
  MaybeAsync,
  Router,
  RouterStateSnapshot,
} from "@angular/router";
import { Store } from "@ngrx/store";
import { map, Observable, of } from "rxjs";
import { selectUser } from "../ngrx/authStore/auth.selector";
import { AuthService } from "../services/AuthService";


@Injectable()
export class AuthGuard implements CanActivate {
  private store = inject(Store);
  private authService = inject(AuthService);
  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    

    const isAuthenticated = localStorage.getItem("access_token") ? true : false;
    if (!isAuthenticated) {
      this.router.navigate(['']);
      return of(false);
    }
    const user = this.authService.decodeToken(localStorage.getItem("access_token") || ''); 
    const expectedRoles = route.data['roles'] as string[];
    if (expectedRoles && expectedRoles.length > 0) {
      const userRoles = user?.role ? user.role.split(',') : [];
      const hasRole = expectedRoles.some(role => userRoles.includes(role));
      if (!hasRole) {
        this.router.navigate(['']);
        return of(false);
      }
    }
    return of(true);
  }
}
