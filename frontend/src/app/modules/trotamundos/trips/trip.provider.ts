import { ApiFacade } from '@core/interfaces/facade.interface';
import { TripVM } from './trip.vm';

export class TrotamundosTripFacade extends ApiFacade<TripVM> {
  constructor() {
    super([], 'trotamundos/trips');
  }

  protected override withData(data: TripVM[]): TripVM[] {
    return data
      .map((e) => new TripVM(e))
      .sort((a, b) => {
        const byName = a.name.localeCompare(b.name);
        if (byName !== 0) return byName;
        const byTitle = a.title.localeCompare(b.title);
        if (byTitle !== 0) return byTitle;
        return a.subtitle.localeCompare(b.subtitle);
      })
      .slice();
  }
}
