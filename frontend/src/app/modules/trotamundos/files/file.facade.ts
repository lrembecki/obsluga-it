import { ArrayApiFacade } from 'app/core/interfaces/facade.interface';
import { FileVM } from './file.vm';

export class TrotamundosFileFacade extends ArrayApiFacade<FileVM> {
  constructor() {
    super([], 'trotamundos/files');
  }

  protected override withData(data: FileVM[]): FileVM[] {
    return data.sort((a, b) => {
      const aPos = typeof a.position === 'number' ? a.position : 0;
      const bPos = typeof b.position === 'number' ? b.position : 0;

      if (aPos !== bPos) return aPos - bPos;

      const aKey = (a.displayName ?? a.group ?? '').toString();
      const bKey = (b.displayName ?? b.group ?? '').toString();
      return aKey.localeCompare(bKey);
    });
  }
}
