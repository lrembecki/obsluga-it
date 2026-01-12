import { ArrayApiFacade } from '../interfaces/facade.interface';
import { TagModel } from '../models/tag.model';

export class TagsFacade extends ArrayApiFacade<TagModel> {
  constructor() {
    super([], 'tags');
  }

  public async add(name: string): Promise<void> {
    this._loading.set(true);

    const response = await this._api.post<TagModel>(
      this._endpoint,
      new TagModel({ name }),
    );

    this._loading.set(false);

    if (response.success) {
      this._data.set([...this._data(), new TagModel(response.data)]);
    }
  }
}
