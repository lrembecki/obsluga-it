import { EnvironmentProviders, inject, Provider } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { FormFacadeScope, FormReturnRouteScope, FormSchemaScope } from "@app/shared/forms/form-schema.model";
import { ApiFacade } from "app/core/interfaces/facade.interface";
import { advantageFormSchema } from "./advantage-form.schema";
import { AdvantageVM } from "./advantage.vm";

export class TrotamundosAdvantageFacade extends ApiFacade<AdvantageVM> {
    constructor() {
        super([], 'trotamundos/advantages');
    }
}

export function provideTrotamundosAdvantages(): (Provider | EnvironmentProviders)[] {
    return [
        TrotamundosAdvantageFacade,
        { provide: FormSchemaScope, useValue: advantageFormSchema },
        { provide: FormReturnRouteScope, useValue: ['/modules/trotamundos/advantages'] },
        { provide: FormFacadeScope, useExisting: TrotamundosAdvantageFacade },
    ];
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