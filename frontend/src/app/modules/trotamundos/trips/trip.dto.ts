import { TripVM } from './trip.vm';

export class TripDTO {
    name: string = '';
    isActive: boolean = false;
    isDisabled: boolean = false;
    title: string = '';
    subtitle: string = '';
    description: string = '';
    startDate: Date = null!;
    endDate: Date = null!;
    calendar: string | null = null;

    constructor(init?: Partial<TripDTO>) { 
        Object.assign(this, init);
    }

    static fromVM(vm: TripVM): TripDTO {
        return new TripDTO({
            name: vm.name,
            isActive: vm.isActive,
            isDisabled: vm.isDisabled,
            title: vm.title,
            subtitle: vm.subtitle,
            description: vm.description,
            startDate: vm.startDate,
            endDate: vm.endDate,
            calendar: (vm.calendar?.length ?? 0) > 0 ? vm.calendar : null,
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
            startDate: dto.startDate,
            endDate: dto.endDate,
            calendar: dto.calendar ?? null,
        });
    }
}
