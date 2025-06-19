import { createAction, props } from "@ngrx/store";
import { User } from "../models/User";

export const loadUsers = createAction('[Users] Loading users');
export const loadUsersSuccess = createAction('[Users] Loading user Success', props<{ users : User[]}>());
export const addUSer = createAction('[User] Add User',props<{ user: User }>());
export const loadUsersFailure = createAction('[Users] Load Users Failure',props<{ error: string }>());
export const setFilter = createAction('[Users] Set Filter', props<{ filter: { role: string; search: string } }>());
