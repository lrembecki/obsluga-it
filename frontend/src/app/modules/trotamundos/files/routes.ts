import { Routes } from '@angular/router';
import { listRoute } from 'app/core/helpers/route.helper';
import { injectTrotamundosFiles, provideTrotamundosFiles, TrotamundosFileProvider } from './file.provider';
import { FileVM } from './file.vm';

export const routes: Routes = [
    listRoute<FileVM, TrotamundosFileProvider>(
        provideTrotamundosFiles(),
        (id: string, services: TrotamundosFileProvider) =>
            services.files.data().find(e => e.id === id)!,
        injectTrotamundosFiles,
        () => import('./file-list').then(m => m.FileList)
    )
];
