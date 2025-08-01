import { Routes } from '@angular/router';
import { Cart } from './components/cart/cart';
import { MyOrders } from './components/my-orders/my-orders';
import { Home } from './components/home/home';
import { News } from './components/news/news';

export const routes: Routes = [
    {path:'home',component:Home},
    {path:'cart', component:Cart},
    {path:'my-orders', component:MyOrders},
    {path:'news-management', component:News}
];
