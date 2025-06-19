import { Routes } from '@angular/router';
import { Navbar } from './navbar/navbar';
import { UserData } from './user-data/user-data';
import { AddUser } from './add-user/add-user';

export const routes: Routes = [
    { 
        path: '', 
        component: Navbar, 
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'users', component: UserData },
            { path: 'addUser', component: AddUser },
        ]
    },
];
