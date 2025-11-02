import {
  Component,
  contentChild,
  input,
  model,
  TemplateRef,
} from '@angular/core';
import { UiTableColumnCellTemplate } from './ui-table-column-cell-template';

@Component({
  selector: 'app-ui-table-column',
  imports: [],
  template: ``,
  styles: ``,
})
export class UiTableColumn {
  public readonly field = input<string>(null!);
  public readonly text = model<string>(null!);
  public readonly width = input<string | undefined>(null!);
  public readonly minWidth = input<string | undefined>(null!);
  public readonly template = model<any>(UiTableColumnCellTemplate);
  public readonly directive = model<any>(null!);
  public readonly cellTemplate = contentChild<TemplateRef<any>>('cell');
  public readonly filterTemplate = contentChild<TemplateRef<any>>('filter');
}
