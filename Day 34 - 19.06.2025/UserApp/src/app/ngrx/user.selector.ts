import { createFeatureSelector, createSelector } from "@ngrx/store";
import { UserState } from "./UserSate";

export const selectUserState = createFeatureSelector<UserState>('user');

export const selectAllUsers = createSelector(selectUserState,state=>state.users);
export const selectUserLoading = createSelector(selectUserState,state=>state.loading);
export const selectUserError = createSelector(selectUserState,state=>state.error);
export const selectUserFilter = createSelector(selectUserState,state=>state.filter);

export const selectUsers = (state: UserState) => state.users;
export const selectFilter = (state: UserState) => state.filter;

export const selectFilteredUsers = createSelector(
  selectAllUsers,
  selectUserFilter,
  (users, filter) => {
    return users.filter(user => {
      const roleMatch = filter.role === 'all' || user.role === filter.role;
      const searchMatch =
        user.username.toLowerCase().includes(filter.search.toLowerCase()) ||
        user.email.toLowerCase().includes(filter.search.toLowerCase());
      return roleMatch && searchMatch;
    });
  }
);
