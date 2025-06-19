import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { UserState } from '../ngrx/UserSate';
import { User } from '../models/User';
import { selectFilteredUsers } from '../ngrx/user.selector';
import { setFilter, loadUsers } from '../ngrx/user.action';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-user-data',
  imports: [AsyncPipe, NgIf, NgFor],
  templateUrl: './user-data.html',
  styleUrls: ['./user-data.css'],
})
export class UserData implements OnInit, OnDestroy {
  users$: Observable<User[]>;
  private searchSubject = new Subject<string>();
  private destroy$ = new Subject<void>();

  currentRole = 'all';

  constructor(private store: Store<UserState>) {
    this.users$ = this.store.select(selectFilteredUsers);
  }

  ngOnInit(): void {
    this.store.dispatch(setFilter({ filter: { role: 'all', search: '' } }));
    this.store.dispatch(loadUsers());

    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe(searchText => {
      this.store.dispatch(setFilter({ filter: { role: this.currentRole, search: searchText } }));
    });
  }

  onRoleChange(role: string): void {
    this.currentRole = role;
    this.store.dispatch(setFilter({ filter: { role, search: '' } }));
  }

  handleSearch(search: string): void {
    this.searchSubject.next(search);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
