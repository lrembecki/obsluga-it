import { EnvironmentProviders, inject, Provider } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ApiFacade } from "app/core/interfaces/facade.interface";
import { TripVM } from "./trip.vm";

export class TrotamundosTripFacade extends ApiFacade<TripVM[]> {
    constructor() {
        super([], 'trotamundos/trips');
    }

    protected override withData(data: TripVM[]): TripVM[] {
        return [...data].sort((a, b) => {
            const byName = a.name.localeCompare(b.name);
            if (byName !== 0) return byName;
            const byTitle = a.title.localeCompare(b.title);
            if (byTitle !== 0) return byTitle;
            return a.subtitle.localeCompare(b.subtitle);
        });
    }
}

export function provideTrotamundosTrips(): (Provider | EnvironmentProviders)[] {
    return [TrotamundosTripFacade];
}

export function injectTrotamundosTrips(): TrotamundosTripProvider {
    return {
        trips: inject(TrotamundosTripFacade),
        router: inject(Router),
        formBuilder: inject(FormBuilder),
        activatedRoute: inject(ActivatedRoute),
    };
}

export type TrotamundosTripProvider = {
    trips: TrotamundosTripFacade,
    router: Router,
    formBuilder: FormBuilder,
    activatedRoute: ActivatedRoute,
};
