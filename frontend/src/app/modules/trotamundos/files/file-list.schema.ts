import { DataTableSchema } from '@app/shared/data-table/data-table.types';
import { FileVM } from './file.vm';

export function fileListSchema(): DataTableSchema<FileVM> {
  return {
    quicksearch: true,
    sortable: true,
    columns: [
      {
        field: 'position',
        label: 'Position',
        type: 'number',
        width: '80px',
        sortable: true,
      },
      {
        field: 'displayName',
        label: 'Display Name',
        type: 'text',
        width: '300px',
        sortable: true,
      },
      {
        field: 'group',
        label: 'Group',
        type: 'text',
        width: '200px',
        sortable: true,
      },
      {
        field: 'description',
        label: 'Description',
        type: 'text',
        width: '300px',
        sortable: true,
      },
    ],
    persistenceKey: 'trotamundos-files',
  };
}
