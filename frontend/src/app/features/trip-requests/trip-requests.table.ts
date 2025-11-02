import { effect, inject, Injectable } from '@angular/core';
import { TripRequestFacade } from 'app/core/facades/trip-request.facade';
import { TripRequestModel } from 'app/core/models/trip-request.model';
import { ButtonPanelService } from '../../shared/ui/button-panel/button-panel';
import {
  TableColumnModel,
  TableService,
} from '../../shared/ui/table/table.service';

@Injectable()
export class FileButtonPanelService extends ButtonPanelService {
  constructor() {
    super();
    this.data.set([
      {
        label: this._translation.instant('UI.EMAIL'),
        field: 'email',
        navigate: (record: TripRequestModel) => [`${record.tripRequestId}`],
      },
    ]);
  }
}

@Injectable()
export class TripRequestsTableService extends TableService<TripRequestModel> {
  private readonly _facade = inject(TripRequestFacade);

  constructor() {
    super();
    effect(() => this.data.set(this._facade.data()));
    effect(() => this.loading.set(this._facade.loading()));

    this.columns.set([
      new TableColumnModel({
        label: this._translation.instant('UI.EMAIL'),
        field: 'email',
        navigate: (record: TripRequestModel) => [`${record.tripRequestId}`],
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('UI.STATUS'),
        field: 'status',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.FIRST_AND_LAST_NAME'),
        field: 'firstAndLastName',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.PHONE_NUMBER'),
        field: 'phoneNumber',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.TRIP_DIRECTION'),
        field: 'tripDirection',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.TRIP_DATE'),
        field: 'tripDate',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.TRIP_DURATION'),
        field: 'tripDuration',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.AMOUNT_OF_PEOPLE'),
        field: 'amountOfPeople',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.AMOUNT_OF_KIDS'),
        field: 'amountOfKids',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.GROUP_RELATION_TYPE'),
        field: 'groupRelationType',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.PREFERRED_AIRPORT'),
        field: 'preferedAirport',
        minWidth: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('TRIP_REQUEST.PREFERRED_TRIP_TYPE'),
        field: 'preferedTripType',
        minWidth: '150px',
      }),
    ]);
  }
}
