import { Validators } from "@angular/forms";
import { FormSchema, TextFormFieldSchema } from "@app/shared/forms/form-schema.model";
import { HighlightVM } from "./highlight.vm";

export const highlightFormSchema = new FormSchema<HighlightVM>({
    layout: 'two-column',
    fields: [
        new TextFormFieldSchema({
            key: 'title',
            label: 'Title',
            validators: [Validators.required]
        }),
        new TextFormFieldSchema({
            key: 'icon',
            label: 'Icon',
            validators: [Validators.required]
        })
    ]
});