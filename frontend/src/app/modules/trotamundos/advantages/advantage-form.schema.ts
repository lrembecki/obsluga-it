import { Validators } from '@angular/forms';
import { FormSchema, TextareaFieldSchema, TextFormFieldSchema } from '@app/shared/forms/form-schema.model';
import { AdvantageVM } from './advantage.vm';

export const advantageFormSchema = new FormSchema<AdvantageVM>({
    layout: 'single-column',
    fields: [
        new TextFormFieldSchema({
            key: 'title',
            label: 'Title',
            placeholder: '',
            validators: [Validators.required],
            dynamicDisabled: (data) => !!data?.id && !data.title,
        }),
        new TextareaFieldSchema({
            key: 'content',
            label: 'Content',
            placeholder: '',
            validators: [Validators.required],
        }),
    ],
    canDelete: (data) => !!data?.id,
});