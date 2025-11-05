import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: 'settings', loadChildren: () => import('./settings/routes').then(m => m.routes) },
    { path: 'trotamundos', loadChildren: () => import('./trotamundos/routes').then(m => m.routes) },
    // Renamed module path from 'security' to 'administration'
    { path: 'administration', loadChildren: () => import('./administration/routes').then(m => m.routes) },
];
