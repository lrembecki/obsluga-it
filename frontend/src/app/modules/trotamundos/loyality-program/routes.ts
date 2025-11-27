import { Routes } from "@angular/router";
import { listRoute } from "app/core/helpers/route.helper";
import { injectTrotamundosLoyalityPrograms, provideTrotamundosLoyalityPrograms, TrotamundosLoyalityProgramProvider } from "./loyality-program.provider";
import { LoyalityProgramVM } from "./loyality-program.vm";

export const routes: Routes = [
    listRoute<LoyalityProgramVM, TrotamundosLoyalityProgramProvider>(
        provideTrotamundosLoyalityPrograms(),
        injectTrotamundosLoyalityPrograms,
        () => import('./loyality-program-list').then(e => e.LoyalityProgramList)
    )
];
