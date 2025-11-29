import { Validators } from "@angular/forms";
import { CheckboxFormFieldSchema, FormSchema, TextFormFieldSchema } from "@app/shared/forms/form-schema.model";
import { EmailSettingsModel } from "./email-settings.model";

export const emailFormSchema = new FormSchema<EmailSettingsModel>({
    layout: 'single-column',
    fields: [
        new CheckboxFormFieldSchema({
            key: 'isActive',
            label: 'Active'
        }),
        new TextFormFieldSchema({
            key: 'smtpServer',
            label: 'SMTP Server',
            validators: [Validators.required]
        }),
        new TextFormFieldSchema({
            key: 'smtpPort',
            label: 'SMTP Port',
            validators: [Validators.required]
        }),
        new TextFormFieldSchema({
            key: 'smtpUsername',
            label: 'SMTP Username',
            validators: [Validators.required]
        }),
        new TextFormFieldSchema({
            key: 'smtpPassword',
            label: 'SMTP Password',
            validators: [Validators.required]
        }),
        new TextFormFieldSchema({
            key: 'fromAddress',
            label: 'From Address',
            validators: [Validators.required, Validators.email]
        }),
        new TextFormFieldSchema({
            key: 'fromName',
            label: 'From Name',
            validators: [Validators.required]
        }),
        new TextFormFieldSchema({
            key: 'replyToAddress',
            label: 'Reply To Address',
            validators: [Validators.email]
        }),
        new TextFormFieldSchema({
            key: 'replyToName',
            label: 'Reply To Name'
        })
    ]
})