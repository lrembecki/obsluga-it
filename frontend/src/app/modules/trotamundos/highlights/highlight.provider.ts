import { EnvironmentProviders, inject, Provider } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ApiFacade } from "app/core/interfaces/facade.interface";
import { HighlightVM } from "./highlight.vm";

export class TrotamundosHighlightFacade extends ApiFacade<HighlightVM[]> {
    constructor() {
        super([], 'trotamundos/highlights');
    }
}

export function provideTrotamundosHighlights(): (Provider | EnvironmentProviders)[] {
    return [TrotamundosHighlightFacade];
}

export function injectTrotamundosHighlights(): TrotamundosHighlightProvider {
    return {
        highlights: inject(TrotamundosHighlightFacade),
        router: inject(Router),
        formBuilder: inject(FormBuilder),
        activatedRoute: inject(ActivatedRoute),
    };
}

export type TrotamundosHighlightProvider = {
    highlights: TrotamundosHighlightFacade,
    router: Router,
    formBuilder: FormBuilder,
    activatedRoute: ActivatedRoute,
}
