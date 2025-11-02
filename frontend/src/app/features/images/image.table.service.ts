import { effect, inject } from '@angular/core';
import { ImageFacade } from 'app/core/facades/image.facade';
import { TagsFacade } from 'app/core/facades/tags.facade';
import { ImageModel } from 'app/core/models/image.model';
import { TagModel } from 'app/core/models/tag.model';
import {
  TableColumnModel,
  TableService,
} from '../../shared/ui/table/table.service';

export class ImageTableService extends TableService<ImageModel> {
  private readonly _facade = inject(ImageFacade);
  private readonly _tags = inject(TagsFacade);

  constructor() {
    super();
    effect(() => {
      this._tags.data();
      this.data.set(
        this._facade.data().map((e) => ({
          ...e,
          _tags: this._tags.data().filter((y) => e.tags.includes(y.name)),
        })),
      );
    });
    effect(() => this.loading.set(this._facade.loading()));

    this.columns.set([
      new TableColumnModel({
        label: this._translation.instant('UI.IMAGE'),
        image: (record: ImageModel) => record,
        navigate: (record: ImageModel) => [`${record.imageId}`],
        width: '150px',
      }),
      new TableColumnModel({
        label: this._translation.instant('UI.FILENAME'),
        field: 'filename',
        width: '250px',
      }),
      new TableColumnModel({
        label: this._translation.instant('UI.DISPLAY_NAME'),
        field: 'displayName',
        width: '250px',
        text: (record) => record.displayName,
        onChange: async (record: ImageModel, input: Record<string, unknown>) =>
          await this.update(record, input),
      }),
      new TableColumnModel({
        label: this._translation.instant('UI.TAGS'),
        virtualField: {
          type: 'tags',
          field: '_tags',
        },
        onChange: async (record: ImageModel, input: Record<string, unknown>) =>
          await this.update(record, input),
        width: '250px',
      }),
      new TableColumnModel({
        label: this._translation.instant('UI.DESCRIPTION'),
        field: 'description',
        text: (record) => record.description,
        onChange: async (record: ImageModel, input: Record<string, unknown>) =>
          await this.update(record, input),
      }),
    ]);
  }

  private async update(
    record: ImageModel,
    input: Record<string, unknown>,
  ): Promise<void> {
    const model = JSON.parse(JSON.stringify(record));

    const tags = input['tags'] as TagModel[];
    if (tags?.length) {
      Object.assign(model, {
        tags: tags.map((e) => e.name),
      });
    }

    if ('displayName' in input) {
      model.displayName = input['displayName'];
    }

    if ('description' in input) {
      model.description = input['description'];
    }

    await this._facade.update(`/${record.imageId}`, model);
  }
}
