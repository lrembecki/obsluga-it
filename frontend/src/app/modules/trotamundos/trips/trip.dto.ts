import { TripVM } from './trip.vm';

export class TripDTO {
    name: string = '';
    isActive: boolean = false;
    isDisabled: boolean = false;
    title: string = '';
    subtitle: string = '';
    description: string = '';

    constructor(init?: Partial<TripDTO>) { Object.assign(this, init); }

    static fromVM(vm: TripVM): TripDTO {
        return new TripDTO({
            name: vm.name,
            isActive: vm.isActive,
            isDisabled: vm.isDisabled,
            title: vm.title,
            subtitle: vm.subtitle,
            description: vm.description,
        });
    }

    static toVM(dto: TripDTO, id?: string): TripVM {
        return new TripVM({
            id: id ?? null!,
            name: dto.name,
            isActive: dto.isActive,
            isDisabled: dto.isDisabled,
            title: dto.title,
            subtitle: dto.subtitle,
            description: dto.description,
        });
    }
}
