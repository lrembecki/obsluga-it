import { EnvironmentProviders, inject, Provider } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ApiFacade } from "app/core/interfaces/facade.interface";
import { LoyalityProgramVM } from "./loyality-program.vm";

export class TrotamundosLoyalityProgramFacade extends ApiFacade<LoyalityProgramVM> {
    constructor() {
        // Assumed endpoint pluralization
        super([], 'trotamundos/loyality-programs');
    }
}

export function provideTrotamundosLoyalityPrograms(): (Provider | EnvironmentProviders)[] {
    return [TrotamundosLoyalityProgramFacade];
}

export function injectTrotamundosLoyalityPrograms(): TrotamundosLoyalityProgramProvider {
    return {
        loyalityPrograms: inject(TrotamundosLoyalityProgramFacade),
        router: inject(Router),
        formBuilder: inject(FormBuilder),
        activatedRoute: inject(ActivatedRoute)
    };
}

export type TrotamundosLoyalityProgramProvider = {
    loyalityPrograms: TrotamundosLoyalityProgramFacade,
    router: Router,
    formBuilder: FormBuilder,
    activatedRoute: ActivatedRoute,
};
