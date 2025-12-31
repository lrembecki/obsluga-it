import { DataTableService } from '@app/shared/data-table/data-table.service';
import { EmailTemplateVM } from './email-template.vm';
export class SettingsEmailTemplateDataTableService extends DataTableService<EmailTemplateVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'name',
          label: 'Name',
          renderLink: (record) => ['..', record.id],
        },
        {
          field: 'fields',
          label: 'Fields',
          type: 'custom',
          render: (row) => String(row.fields?.length ?? 0),
          width: '140px',
        },
        {
          field: 'templateHtml',
          label: 'Template',
          type: 'custom',
          render: (row) => row.templateHtml?.filename ?? '',
          renderLink: (record) => ({
            url: record.templateHtml.blobUrl!,
          }),
        },
      ],
      persistenceKey: 'settings-email-templates',
    });
  }
}
