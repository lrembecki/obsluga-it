export type TripRequestStatus = 'New';

export class TripRequestModel {
  tripRequestId!: string; // Guid as string
  status: TripRequestStatus = 'New';
  email: string = '';
  firstAndLastName: string = '';
  phoneNumber: string = '';
  tripDirection: string = '';
  tripDate: string = '';
  tripDuration: string = '';
  amountOfPeople?: number;
  amountOfKids?: number;
  groupRelationType: string = '';
  preferedAirport: string = '';
  preferedTripType: string = '';
}
