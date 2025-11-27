import { Routes } from '@angular/router';
import { listRoute } from '@core/helpers/route.helper';
import {
  injectTrotamundosTrips,
  provideTrotamundosTrips,
  TrotamundosTripProvider,
} from './trip.provider';
import { TripVM } from './trip.vm';

export const routes: Routes = [
  listRoute<TripVM, TrotamundosTripProvider>(
    provideTrotamundosTrips(),
    injectTrotamundosTrips,
    () => import('./trip-list').then(e => e.TripList)
  )
];
