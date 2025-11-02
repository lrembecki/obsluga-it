import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { ImageFacade } from 'app/core/facades/image.facade';
import { TableService } from '../../shared/ui/table/table.service';
import { ImageTableService } from './image.table.service';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('app/shared/ui/list/list').then((e) => e.List),
    resolve: {
      init: () => {
        const facade = { images: inject(ImageFacade) };

        Promise.allSettled(Object.values(facade).map((e) => e.populate()));
      },
    },
    providers: [
      ImageTableService,
      { provide: TableService, useExisting: ImageTableService },
    ],
  },
  {
    path: ':imageId',
    loadComponent: () => import('./image-form').then((e) => e.ImageForm),
    resolve: {
      model: async (state: ActivatedRouteSnapshot) => {
        const images = inject(ImageFacade);

        await images.initialize();

        const imageId = state['params']!['imageId'] as string;
        return images.data().find((e) => e.imageId === imageId)!;
      },
    },
  },
];
