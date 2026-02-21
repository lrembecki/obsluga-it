import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { FileVM } from './file.vm';

export class TrotamundosFileFacade extends ArrayApiFacade<FileVM> {
  constructor() { super([], 'trotamundos/files'); }
  protected override withData(data: FileVM[]): FileVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
