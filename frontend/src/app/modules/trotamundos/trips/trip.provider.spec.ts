import { TrotamundosTripFacade } from './trip.provider';
import { TripVM } from './trip.vm';

class TestFacade extends TrotamundosTripFacade {
    public testWithData(data: TripVM[]) {
        // @ts-ignore accessing protected for test via public wrapper
        return this.withData(data);
    }
}

describe('TrotamundosTripFacade', () => {
    it('sorts by title then subtitle', () => {
        const facade = new TestFacade();
        const unsorted = [
            new TripVM({ id: '1', title: 'B', subtitle: 'z', description: '' }),
            new TripVM({ id: '2', title: 'A', subtitle: 'b', description: '' }),
            new TripVM({ id: '3', title: 'A', subtitle: 'a', description: '' }),
        ];
        const sorted = facade.testWithData(unsorted);
        expect(sorted.map(e => e.id)).toEqual(['3', '2', '1']);
    });
});
