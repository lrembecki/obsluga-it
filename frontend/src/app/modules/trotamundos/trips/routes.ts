import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { routeFeature, routeList } from '@core/helpers/route.helper';
import { TrotamundosTripFacade } from './trip.provider';
import { TripVM } from './trip.vm';

let featureRoute = routeFeature(TrotamundosTripFacade);

routeList(
  [TrotamundosTripFacade],
  () => ({
    facade: inject(TrotamundosTripFacade),
    columns: [
      {
        label: 'Name',
        field: 'name',
        width: '180px',
        link: { renderLink: (record: TripVM) => ['..', record.id] },
      },
      { label: 'Title', field: 'title' },
      { label: 'Subtitle', field: 'subtitle' },
      {
        label: 'Start Date',
        field: 'startDate',
        width: '140px',
        render: (row: TripVM) => row.startDate.toLocaleDateString(),
      },
      {
        label: 'End Date',
        field: 'endDate',
        width: '140px',
        render: (row: TripVM) => row.endDate.toLocaleDateString(),
      },
      { label: 'Calendar', field: 'calendar', width: '160px' },
      { label: 'Active', field: 'isActive', width: '100px' },
      { label: 'Disabled', field: 'isDisabled', width: '110px' },
    ],
  }),
  featureRoute,
);

export const routes: Routes = [featureRoute];
