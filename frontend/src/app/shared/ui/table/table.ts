import { NgTemplateOutlet } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ImageModel } from 'app/core/models/image.model';
import { TagsMultiselectInput } from 'app/features/tags/tags-multiselect-input';
import { TableModule } from 'primeng/table';
import { CachedImageComponent } from '../cached-image/cached-image.component';
import { TextInputComponent } from '../inputs/text-input.component';
import { TableColumnModel, TableService } from './table.service';

@Component({
  selector: 'app-table',
  imports: [
    TableModule,
    NgTemplateOutlet,
    RouterLink,
    CachedImageComponent,
    TagsMultiselectInput,
    TextInputComponent,
  ],
  template: `
    <p-table
      [value]="data()"
      [loading]="loading()"
      [tableStyle]="{ 'min-width': '50rem' }"
    >
      <ng-template #header>
        <tr>
          @for (column of columns(); track column) {
            <th
              [style.min-width]="column.minWidth"
              [style.width]="column.width"
            >
              {{ column.label }}
            </th>
          }
        </tr>
      </ng-template>
      <ng-template #body let-record let-rowIndex="rowIndex">
        <tr>
          @for (column of columns(); track column) {
            <td>
              @if (column.navigate) {
                <ng-container
                  [ngTemplateOutlet]="link_template"
                  [ngTemplateOutletContext]="{
                    column,
                    record,
                    routerLink: column.navigate(record),
                    rowIndex,
                  }"
                />
              } @else {
                <ng-container
                  [ngTemplateOutlet]="render_Template"
                  [ngTemplateOutletContext]="{ column, record, rowIndex }"
                />
              }

              @if (column.buttons?.length) {
                <div class="table-tr-td-button-container"></div>
              }
            </td>
          }
        </tr>
      </ng-template>
    </p-table>

    <ng-template
      #link_template
      let-column="column"
      let-record="record"
      let-routerLink="routerLink"
      let-rowIndex="rowIndex"
    >
      <a [routerLink]="routerLink">
        <ng-container
          [ngTemplateOutlet]="render_Template"
          [ngTemplateOutletContext]="{ column, record, rowIndex }"
        />
      </a>
    </ng-template>

    <ng-template
      #render_Template
      let-column="column"
      let-record="record"
      let-rowIndex="rowIndex"
    >
      @if (column.text) {
        <app-text-input
          [value]="column.text(record)"
          (valueChange)="onChange(record, $event, column)"
        />
      } @else if (column.tags) {
        <app-tags-multiselect-input
          [value]="column.tags(record)"
          (valueChange)="column.onChange(record, { tags: $event })"
        />
      } @else if (column.field) {
        {{ column.render(record) }}
      } @else if (column.join) {
        {{ column.join(record).join(', ') }}
      } @else if (column.virtualField) {
        @if (column.virtualField.type === 'tags') {
          <app-tags-multiselect-input
            [value]="record[column.virtualField.field]"
            (valueChange)="column.onChange(record, { tags: $event })"
          />
        }
      } @else if (column.image) {
        @let image = getImage(record, column)!;
        <app-cached-image
          [src]="image.blobUrl"
          [alt]="image.filename"
          [width]="image.width || 150"
          [height]="image.height || 100"
          loading="lazy"
          [priority]="isFirstImage(rowIndex, column)"
        >
        </app-cached-image>
      }
    </ng-template>
  `,
  styles: `
    ::ng-deep app-table img {
      max-height: 100px;
      min-width: 100px;
    }

    .table-tr-td-button-container {
      display: flex;
      gap: 0.5rem;
    }
  `,
})
export class Table<T> {
  private readonly _service = inject(TableService<T>);
  private _firstImageDetected = false;

  protected readonly columns = this._service.columns.asReadonly();
  protected readonly data = this._service.data.asReadonly();
  protected readonly loading = this._service.loading.asReadonly();

  protected getImage(
    record: unknown,
    column: TableColumnModel<unknown>,
  ): ImageModel {
    return column.image!(record);
  }

  protected isFirstImage(
    rowIndex: number,
    column: TableColumnModel<unknown>,
  ): boolean {
    // Only mark the first image in the table as priority for LCP optimization
    if (this._firstImageDetected || !column.image) {
      return false;
    }

    // Check if this is the first row with an image
    if (rowIndex === 0) {
      this._firstImageDetected = true;
      return true;
    }

    return false;
  }

  protected timestamp: Date = null!;
  protected onChange(record: T, value: any, column: TableColumnModel<T>) {
    const date = new Date();
    this.timestamp = date;

    setTimeout(() => {
      if (+date === +this.timestamp) {
        if (column.onChange) {
          column.onChange(record, { [column.field]: value });
        }
      }
    }, 500);
  }
}
