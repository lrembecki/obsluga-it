import { TripVM } from './trip.vm';

describe('TripVM', () => {
  it('initializes optional scheduling fields as null', () => {
    const vm = new TripVM();
    expect(vm.startDate).toBeNull();
    expect(vm.endDate).toBeNull();
    expect(vm.calendar).toBeNull();
  });

  it('applies partial initialization', () => {
    const vm = new TripVM({ startDate: new Date('2025-01-01'), calendar: 'Summer' });
    expect(vm.startDate).toEqual(new Date('2025-01-01'));
    expect(vm.calendar).toBe('Summer');
    expect(vm.endDate).toBeNull();
  });
});