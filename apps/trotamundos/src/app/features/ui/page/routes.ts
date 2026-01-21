import { Routes } from '@angular/router';
import { WebsiteFacade } from '../../../core/facades/website.facade';
import { inject } from '@angular/core';
import { ApiService } from '../../../core';
import { SeoService } from '../../../core/services/seo.service';
import { NavbarService } from '@/app/core/services/navbar.service';

export const routes: Routes = [
  {
    path: '',
    providers: [WebsiteFacade, ApiService, SeoService, NavbarService],
    resolve: {
      website: async () => await inject(WebsiteFacade).initialize(),
    },
    loadComponent: () => import('./page').then((m) => m.Page),
    children: [
      {
        path: 'o-nas',
        loadComponent: () => import('@features/pages/about-us/about-us').then((m) => m.AboutUs),
      },
      {
        path: 'jak-to-dziala',
        loadComponent: () =>
          import('@features/pages/how-it-works/how-it-works').then((m) => m.HowItWorks),
      },
      {
        path: 'kontakt',
        loadComponent: () => import('@features/pages/contact/contact').then((m) => m.Contact),
      },
    ],
  },
];
