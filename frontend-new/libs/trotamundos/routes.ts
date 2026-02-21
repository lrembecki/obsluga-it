import { Routes } from '@angular/router';

export const trotamundosRoutes: Routes = [
  {
    path: 'advantages',
    data: { label: 'Advantages', permissions: ['Trotamundos.Advantages'] },
    loadChildren: () => import('./advantages/src').then((e) => e.routes),
  },
  {
    path: 'highlights',
    data: { label: 'Highlights', permissions: ['Trotamundos.Highlights'] },
    loadChildren: () => import('./highlights/src').then((e) => e.routes),
  },
  {
    path: 'loyality-program',
    data: { label: 'Loyality Program', permissions: ['Trotamundos.Loyality-Program'] },
    loadChildren: () => import('./loyality-program/src').then((e) => e.routes),
  },
  {
    path: 'trips',
    data: { label: 'Trips', permissions: ['Trotamundos.Trips'] },
    loadChildren: () => import('./trips/src').then((e) => e.routes),
  },
  {
    path: 'files',
    data: { label: 'Files', permissions: ['Trotamundos.Files'] },
    loadChildren: () => import('./files/src').then((e) => e.routes),
  },
  {
    path: 'about-us',
    data: { label: 'About Us', permissions: ['Trotamundos.Pages.AboutUs'] },
    loadChildren: () => import('./about-us/src').then((e) => e.routes),
  },
  {
    path: 'how-it-works',
    data: { label: 'How It Works', permissions: ['Trotamundos.Pages.HowItWorks'] },
    loadChildren: () => import('./how-it-works/src').then((e) => e.routes),
  },
];
