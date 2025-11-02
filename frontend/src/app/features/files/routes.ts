import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { withBreadcrumbs } from 'app/core/helpers/breadcrumbs.helper';
import { FileFacade, FileGroupFacade } from 'app/features/files/file.facade';
import { GroupFacade } from '../groups/group.facade';

export const routes: Routes = withBreadcrumbs([
  {
    path: '',
    resolve: {
      init: () => {
        Promise.allSettled(
          Object.values({
            files: inject(FileFacade),
            fileGroups: inject(FileGroupFacade),
          }).map((e) => e.populate()),
        );
      },
    },
    providers: [{ provide: GroupFacade, useExisting: FileGroupFacade }],
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () => import('./file-list').then((e) => e.FileList),
      },
      {
        path: ':fileId',
        loadComponent: () => import('./file-form').then((e) => e.FileForm),
      },
    ],
  },
]);
