import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const routes: Routes = [
    featureRoute('advantages', 'Advantages', [], () => 
        import('./advantages/routes').then(e => e.routes)
),
];
