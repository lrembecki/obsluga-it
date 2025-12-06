import { Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { FormSchema } from "@app/shared/forms";

@Injectable({ providedIn: 'root' })
export class FormRulesService {
    applyRules<T>(schema: FormSchema<T>, form: FormGroup, value: any) {
        schema.fields.forEach(field => {
            if (field.dynamicDisabled) {
                const shouldDisable = field.dynamicDisabled(value);
                const control = form.get(field.key as string);

                shouldDisable
                    ? control?.disable({ emitEvent: false })
                    : control?.enable({ emitEvent: false });
            }
        });
    }
}