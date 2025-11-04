import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: 'settings', loadChildren: () => import('./settings/routes').then(m => m.routes) },
    { path: 'trotamundos', loadChildren: () => import('./trotamundos/routes').then(m => m.routes) },
    { path: 'security', loadChildren: () => import('./security/routes').then(m => m.routes) },
];
