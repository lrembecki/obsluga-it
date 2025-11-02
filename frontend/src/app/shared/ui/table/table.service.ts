import { inject, Injectable, Provider, signal } from '@angular/core';
import { ImageModel } from 'app/core/models/image.model';
import { TagModel } from 'app/core/models/tag.model';
import { TranslationService } from 'app/core/services/translation.service';
import { MenuItem } from 'primeng/api';
import { Button } from '../button/button';

export function provideTableService<T>(): Provider {
  return TableService<T>;
}

@Injectable()
export class TableService<T> {
  protected readonly _translation = inject(TranslationService);
  public readonly data = signal<T[]>([]);
  public readonly columns = signal<TableColumnModel<T>[]>([]);
  public readonly loading = signal<boolean>(false);
}

export class TableColumnModel<T> {
  label: string = null!;
  field: string = null!;
  minWidth?: string = null!;
  width?: string = null!;
  virtualField: { type: string; field: string } = null!;
  image?: (record: T) => ImageModel = null!;
  navigate?: (record: T) => string[] = null!;
  join?: (record: T) => any[] = null!;
  tags?: (record: T) => TagModel[] = null!;
  text?: (record: T) => string = null!;
  onChange?: (record: T, model: Record<string, unknown>) => Promise<void> =
    async (record: T, model: Record<string, unknown>) =>
      console.log({ record, model });

  buttons?: {
    item: MenuItem;
    isVisible?: (record: T) => boolean;
    render?: (button: Button, record: T) => void;
  }[] = [];

  render: (record: T) => string = (record) =>
    (<any>record)[this.field]?.toString() ?? null!;

  constructor(init?: Partial<TableColumnModel<T>>) {
    Object.assign(this, init);
  }
}
