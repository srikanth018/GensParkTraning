import { Routes } from '@angular/router';
import { Products } from './products/products';
import { About } from './about/about';
import { Navbar } from './navbar/navbar';
import { ProfilePage } from './profile-page/profile-page';
import { LoginPage } from './login-page/login-page';
import { AuthGuard } from './auth-guard';
import { ProductDetails } from './product-details/product-details';

export const routes: Routes = [
    {path:'login', component:LoginPage},
    {path:'home', component:Navbar,children:[
        {path:'products',component:Products},
        {path:'profile',component:ProfilePage},
        {path:'product/:id',component:ProductDetails}
    ],canActivate: [AuthGuard]},

    {path:'about',component:About,children:[
        {path:'products',component:Products}
    ]},
    {path:'about/:un',component:About,children:[
        {path:'products',component:Products}
    ]},

    
    {path:'products',component:Products}



];
