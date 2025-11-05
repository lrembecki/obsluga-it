import { Routes } from '@angular/router';
import { listRoute } from 'app/core/helpers/route.helper';
import { injectTrotamundosTrips, provideTrotamundosTrips, TrotamundosTripProvider } from './trip.provider';
import { TripVM } from './trip.vm';

export const routes: Routes = [
  listRoute<TripVM, TrotamundosTripProvider>(
    provideTrotamundosTrips(),
    (id: string, services: TrotamundosTripProvider) => services.trips.data().find(e => e.id === id)!,
    injectTrotamundosTrips,
    () => import('./trip-list').then(e => e.TripList),
    () => import('./trip-form').then(e => e.TripForm)
  )
];
