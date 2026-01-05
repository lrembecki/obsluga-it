import { Component, computed } from '@angular/core';
import { BaseFormComponent } from '../forms/base-form.component';
import { provideFormService } from '../forms/form.service';
import { DataTableFormService } from './data-table-filter-form.service';

@Component({
  selector: 'app-data-table-form-renderer',
  template: `
    <!-- <app-form-renderer [schema]="schema()" [form]="_service.form()!" /> -->
  `,
  styles: ``,
  providers: [provideFormService(DataTableFormService)],
})
export class DataTableFormRenderer extends BaseFormComponent<any> {
  readonly schema = computed(() => this._service.schema());
  override async submit(data: any): Promise<void> {
    console.log({ data });
  }
}
