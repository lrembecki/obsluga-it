import { EnvironmentProviders, inject, Provider } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ApiFacade } from "app/core/interfaces/facade.interface";
import { AdvantageVM } from "./advantage.vm";

export class TrotamundosAdvantageFacade extends ApiFacade<AdvantageVM[]> {
    constructor() {
        super([], 'trotamundos/advantages');
    }
}

export function provideTrotamundosAdvantages(): (Provider | EnvironmentProviders)[] {
    return [TrotamundosAdvantageFacade];
}

export function injectTrotamundosAdvantages(): TrotamundosAdvantageProvider {
    return {
        advantages: inject(TrotamundosAdvantageFacade),
        router: inject(Router),
        formBuilder: inject(FormBuilder),
        activatedRoute: inject(ActivatedRoute),
    };
}

export type TrotamundosAdvantageProvider = {
    advantages: TrotamundosAdvantageFacade,
    router: Router,
    formBuilder: FormBuilder,
    activatedRoute: ActivatedRoute,
}