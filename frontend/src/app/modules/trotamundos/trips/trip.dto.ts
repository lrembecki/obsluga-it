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
        this.session().images.forEach((img, index) => img.order = index);
        this.update();
    }

    public addAgenda(): void {
        this.session().agenda.push(TripAgendaDTO.create(this.session().agenda.length, ''));
        this.update();
    }

    public removeAgenda(order: number): void {
        this.session().agenda = this.session().agenda.filter(a => a.order !== order);
        this.session().agenda.forEach((ag, index) => ag.order = index);
        this.update();
    }
}

export class TripAgendaDTO {
    order: number = null!;
    content: string = null!;

    constructor(init?: Partial<TripAgendaDTO>) {
        Object.assign(this, init);
    }

    public static create(order: number, content: string): TripAgendaDTO {
        return new TripAgendaDTO({
            order,
            content
        });
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

    agenda: Partial<TripAgendaDTO>[] = [];
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
            agenda: vm.agenda.map((ta, index) => TripAgendaDTO.create(index, ta.content)),
            highlights: vm.highlights.map((th, index) => ({
                order: index,
                highlightId: th.highlightId,
                highlight: highlights.find(h => h.id === th.highlightId) ?? null!,
                value: th.value
            })),
            images: vm.images.map((ti, index) => TripImageDTO.create(index, ti.image))
        });
    }
}
