import { Injectable } from '@angular/core';
import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { TripRequestModel } from '../models/trip-request.model';

@Injectable({ providedIn: 'root' })
export class TripRequestFacade extends ApiFacade<TripRequestModel> {
  constructor() {
    super([], 'trip-requests');
  }
}
