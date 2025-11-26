import { Routes } from "@angular/router";
import { listRoute } from "app/core/helpers/route.helper";
import { injectTrotamundosAdvantages, provideTrotamundosAdvantages, TrotamundosAdvantageProvider } from "./advantage.provider";
import { AdvantageVM } from "./advantage.vm";

export const routes: Routes = [
    listRoute<AdvantageVM, TrotamundosAdvantageProvider>(
        provideTrotamundosAdvantages(),
        (id: string, services: TrotamundosAdvantageProvider) => {
            return services.advantages.data().find(e => e.id === id)!;
        },
        injectTrotamundosAdvantages,
        () => import('./advantage-list').then(e => e.AdvantageList)
    )
];