import { computed, signal } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { routeTemplate } from 'app/core/helpers/route.helper';
import { ContactModel } from './contact.model';
import {
  injectSettingContacts,
  provideSettingContacts,
} from './contact.provider';

export const routes: Routes = [
  routeTemplate({
    providers: [provideSettingContacts()],
    serviceProvider: () => injectSettingContacts(),
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full',
      },
      {
        path: 'list',
        loadComponent: () =>
          import('./contact-list').then((e) => e.ContactList),
      },
      {
        path: 'create',
        loadComponent: () =>
          import('./contact-form').then((e) => e.ContactForm),
        resolve: {
          model: () => signal<ContactModel>(null!),
        },
      },
      {
        path: ':contactId',
        loadComponent: () =>
          import('./contact-form').then((e) => e.ContactForm),
        resolve: {
          model: (snapshot: ActivatedRouteSnapshot) => {
            const services = injectSettingContacts();
            const contactId = snapshot.params['contactId'] as string;

            return computed(() => {
              const result = services.contacts
                .data()
                .find((e) => e.id === contactId);

              return result;
            });
          },
        },
      },
    ],
  }),
];
