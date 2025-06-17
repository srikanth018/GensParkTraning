import { Routes } from '@angular/router';
import { Products } from './products/products';
import { About } from './about/about';

export const routes: Routes = [
    {path:'home', component:Products},
    // {path:'about',component:About},
    {path:'home/:un', component:Products},
    {path:'about',component:About,children:[
        {path:'products',component:Products}
    ]},
    {path:'about/:un',component:About,children:[
        {path:'products',component:Products}
    ]}

];
