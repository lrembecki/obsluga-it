import { EnvironmentProviders, inject, Provider } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { FormFacadeScope, FormReturnRouteScope, FormSchemaScope } from "@app/shared/forms/form-schema.model";
import { ApiFacade } from "app/core/interfaces/facade.interface";
import { highlightFormSchema } from "./highlight-form.schema";
import { HighlightVM } from "./highlight.vm";

export class TrotamundosHighlightFacade extends ApiFacade<HighlightVM> {
    constructor() {
        super([], 'trotamundos/highlights');
    }
}

export function provideTrotamundosHighlights(): (Provider | EnvironmentProviders)[] {
    return [
        TrotamundosHighlightFacade,
        { provide: FormSchemaScope, useValue: highlightFormSchema },
        { provide: FormReturnRouteScope, useValue: '/trotamundos/highlights' },
        { provide: FormFacadeScope, useExisting: TrotamundosHighlightFacade },
    ];
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
