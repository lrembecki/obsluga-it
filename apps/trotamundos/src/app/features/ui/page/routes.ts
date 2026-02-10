import { Routes } from '@angular/router';
import { inject } from '@angular/core';
import {
  ApiService,
  CompaniesFacade,
  FormDefinitionsFacade,
  NavbarService,
  SeoService,
  WebsiteFacade,
} from '@app/core';
import { AboutUsFacade } from '@/app/core/facades/about-us.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      WebsiteFacade,
      CompaniesFacade,
      FormDefinitionsFacade,
      ApiService,
      SeoService,
      NavbarService,
      AboutUsFacade,
    ],
    resolve: {
      website: async () => await inject(WebsiteFacade).initialize(),
    },
    loadComponent: () => import('./page').then((m) => m.Page),
    children: [
      {
        path: '',
        loadComponent: () => import('@features/pages/home/home').then((m) => m.Home),
      },
      {
        path: 'o-nas',
        resolve: {
          aboutUs: async () => await inject(AboutUsFacade).initialize(),
        },
        loadComponent: () => import('@features/pages/about-us/about-us').then((m) => m.AboutUs),
      },
      {
        path: 'jak-to-dziala',
        loadComponent: () =>
          import('@features/pages/how-it-works/how-it-works').then((m) => m.HowItWorks),
      },
      {
        path: 'kontakt',
        resolve: {
          company: async () => await inject(CompaniesFacade).initialize(),
          formDefinitions: async () => await inject(FormDefinitionsFacade).initialize(),
        },
        loadComponent: () => import('@features/pages/contact/contact').then((m) => m.Contact),
      },
    ],
  },
];
