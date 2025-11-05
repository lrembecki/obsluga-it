import { TripVM } from './trip.vm';

export class TripDTO {
    title: string = '';
    subtitle: string = '';
    description: string = '';

    constructor(init?: Partial<TripDTO>) { Object.assign(this, init); }

    static fromVM(vm: TripVM): TripDTO {
        return new TripDTO({
            title: vm.title,
            subtitle: vm.subtitle,
            description: vm.description,
        });
    }

    static toVM(dto: TripDTO, id?: string): TripVM {
        return new TripVM({
            id: id ?? null!,
            title: dto.title,
            subtitle: dto.subtitle,
            description: dto.description,
        });
    }
}
