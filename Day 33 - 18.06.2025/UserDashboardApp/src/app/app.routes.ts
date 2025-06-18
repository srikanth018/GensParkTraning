import { Routes } from '@angular/router';
import { UserDashboard } from './components/user-dashboard/user-dashboard';
import { Navbar } from './components/navbar/navbar';
import { AddUser } from './components/add-user/add-user';
import { UserDetails } from './components/user-details/user-details';

export const routes: Routes = [
    { 
        path: '', 
        component: Navbar, 
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'dashboard', component: UserDashboard },
            { path: 'users', component: UserDetails },
            { path: 'addUser', component: AddUser },
        ]
    },
];
