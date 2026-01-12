import { Injectable } from '@angular/core';
import { ArrayApiFacade } from 'app/core/interfaces/facade.interface';
import { TripRequestModel } from '../models/trip-request.model';

@Injectable({ providedIn: 'root' })
export class TripRequestFacade extends ArrayApiFacade<TripRequestModel> {
  constructor() {
    super([], 'trip-requests');
  }
}
