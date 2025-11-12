/* eslint-disable no-unused-vars */
import { ContextModel } from 'app/core/helpers/signal.helper';
import { HighlightVM } from '../highlights/highlight.vm';
import { StorageVM } from '../loyality-program/loyality-program.vm';
import { TripVM } from './trip.vm';

export class TripContextModel extends ContextModel<TripDTO> {
    constructor(
        existing: () => TripDTO,
        onUpdate: (input: TripDTO) => TripDTO = (input) => input
    ) {
        super(existing, onUpdate);
    }

    public addHighlight(highlight: HighlightVM): void {
        this.session().highlights.push(TripHighlightDTO.create(this.session().highlights.length, highlight));
        this.update();
    }

    public removeHighlight(highlightId: string): void {
        this.session().highlights = this.session().highlights.filter(h => h.highlightId !== highlightId);
        this.update();
    }

    public addImage(image: StorageVM): void {
        this.session().images.push(TripImageDTO.create(this.session().images.length, image));
        this.update();
    }

    public removeImage(order: number): void {
        this.session().images = this.session().images.filter(i => i.order !== order);
        this.update();
    }
}

export class TripHighlightDTO {
    highlightId: string = null!;
    highlight: HighlightVM = null!;
    value: string = null!;
    order: number = null!;

    constructor(init?: Partial<TripHighlightDTO>) {
        Object.assign(this, init);
    }

    public static create(order: number, highlight: HighlightVM): TripHighlightDTO {
        return new TripHighlightDTO({
            order,
            highlightId: highlight.id,
            highlight,
            value: null!
        });
    }
}

export class TripImageDTO {
    order: number = null!;
    imageId: string = null!;
    image: StorageVM = null!;

    constructor(init?: Partial<TripImageDTO>) {
        Object.assign(this, init);
    }

    public static create(order: number, image: StorageVM): TripImageDTO {
        return new TripImageDTO({
            order,
            imageId: image.id,
            image
        });
    }
}

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

    highlights: Partial<TripHighlightDTO>[] = [];
    images: Partial<TripImageDTO>[] = [];

    constructor(init?: Partial<TripDTO>) {
        Object.assign(this, init);
    }

    static fromVM(vm: TripVM, highlights: HighlightVM[]): TripDTO {
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
            highlights: vm.highlights.map((th, index) => ({
                order: index,
                highlightId: th.highlightId,
                highlight: highlights.find(h => h.id === th.highlightId) ?? null!,
                value: th.value
            })),
            images: vm.images.map((ti, index) => ({
                order: index,
                imageId: ti.imageId,
                image: ti.image
            }))
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
