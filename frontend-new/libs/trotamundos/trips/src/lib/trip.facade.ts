import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { TripVM } from './trip.vm';

export class TrotamundosTripFacade extends ArrayApiFacade<TripVM> {
  constructor() { super([], 'trotamundos/trips'); }
  protected override withData(data: TripVM[]): TripVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
