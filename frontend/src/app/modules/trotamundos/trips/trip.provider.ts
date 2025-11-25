import { EnvironmentProviders, inject, Provider, signal } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ApiFacade, Facade } from "app/core/interfaces/facade.interface";
import { TableColumn } from "app/shared/templates/table/table-template";
import { TrotamundosAdvantageFacade } from "../advantages/advantage.provider";
import { TrotamundosHighlightFacade } from "../highlights/highlight.provider";
import { TripVM } from "./trip.vm";

export class TrotamundosTripFacade extends ApiFacade<TripVM[]> {
    constructor() {
        super([], 'trotamundos/trips');
    }

    protected override withData(data: TripVM[]): TripVM[] {
        return data
            .map(e => new TripVM(e))
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

export class TrotamundosTripColumnsFacade implements Facade<TableColumn<TripVM>[]> {

    public readonly data = signal<TableColumn<TripVM>[]>(TableColumn.fromArray<TripVM>([
        { text: 'Name', field: 'name', width: '180px', link: { renderLink: (record) => ['..', record.id] } },
        { text: 'Title', field: 'title' },
        { text: 'Subtitle', field: 'subtitle' },
        { text: 'Start Date', field: 'startDate', date: { format: 'shortDate' }, width: '140px' },
        { text: 'End Date', field: 'endDate', date: { format: 'shortDate' }, width: '140px' },
        { text: 'Calendar', field: 'calendar', width: '160px' },
        { text: 'Active', field: 'isActive', width: '100px' },
        { text: 'Disabled', field: 'isDisabled', width: '110px' }
    ]));
    public readonly loading = signal(false);
    public readonly initialized = signal(true);

    async initialize(): Promise<void> {
        // No initialization needed
    }
    async populate(): Promise<this> {
        // No population needed
        return this;
    }
}

export function provideTrotamundosTrips(): (Provider | EnvironmentProviders)[] {
    return [TrotamundosTripFacade, TrotamundosHighlightFacade, TrotamundosTripColumnsFacade];
}

export function injectTrotamundosTrips(): TrotamundosTripProvider {
    return {
        trips: inject(TrotamundosTripFacade),
        router: inject(Router),
        formBuilder: inject(FormBuilder),
        activatedRoute: inject(ActivatedRoute),
        highlights: inject(TrotamundosHighlightFacade),
        columns: inject(TrotamundosTripColumnsFacade),
        advantages: inject(TrotamundosAdvantageFacade)
    };
}

export type TrotamundosTripProvider = {
    trips: TrotamundosTripFacade,
    router: Router,
    formBuilder: FormBuilder,
    activatedRoute: ActivatedRoute,
    highlights: TrotamundosHighlightFacade,
    columns: TrotamundosTripColumnsFacade,
    advantages: TrotamundosAdvantageFacade
};