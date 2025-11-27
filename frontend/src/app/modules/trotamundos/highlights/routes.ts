import { Routes } from "@angular/router";
import { listRoute } from "app/core/helpers/route.helper";
import { injectTrotamundosHighlights, provideTrotamundosHighlights, TrotamundosHighlightProvider } from "./highlight.provider";
import { HighlightVM } from "./highlight.vm";

export const routes: Routes = [
    listRoute<HighlightVM, TrotamundosHighlightProvider>(
        provideTrotamundosHighlights(),
        injectTrotamundosHighlights,
        () => import('./highlight-list').then(e => e.HighlightList)
    )
];
