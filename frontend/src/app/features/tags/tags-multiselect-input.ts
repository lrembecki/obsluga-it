import { Component, inject, model, output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TagsFacade } from 'app/core/facades/tags.facade';
import { TagModel } from 'app/core/models/tag.model';
import { HostControlDirective } from '../../shared/ui/inputs/host-control.directive';
import { MultiSelectInputComponent } from '../../shared/ui/inputs/multi-select-input.component';

@Component({
  selector: 'app-tags-multiselect-input',
  imports: [MultiSelectInputComponent, ReactiveFormsModule],
  hostDirectives: [HostControlDirective],
  template: `
    <app-multi-select-input
      [data]="tags()"
      textField="name"
      (addElement)="addElement($event)"
      [loading]="loading()"
      [value]="value()"
      (valueChange)="onChange($event)"
      [formControl]="hcd.control"
    />
  `,
  styles: `
    :host {
      display: flex;
      width: 100%;
    }

    ::ng-deep app-tags-multiselect-input > * {
      display: flex;
    }

    ::ng-deep app-tags-multiselect-input > *,
    ::ng-deep .p-component {
      flex: auto;
    }
  `,
})
export class TagsMultiselectInput {
  protected readonly hcd = inject(HostControlDirective);
  private readonly _tags = inject(TagsFacade);
  protected readonly tags = this._tags.data;
  protected readonly loading = this._tags.loading;

  public readonly value = model<TagModel[]>([]);
  public readonly valueChange = output<TagModel[]>();

  protected onChange(event: TagModel[]): void {
    const data = event.map((e) => e.name);
    const value = this.value().map((e) => e.name);

    if ((data || '').join(',') !== (value || '').join(',')) {
      this.value.set(event);
      this.valueChange.emit(this.value());
    }
  }
  protected async addElement(event: string): Promise<void> {
    await this._tags.add(event);
  }
}
