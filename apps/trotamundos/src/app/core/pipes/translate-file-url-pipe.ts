import { Pipe, PipeTransform } from '@angular/core';
import { StorageVM } from '../models/storage.model';

@Pipe({
  name: 'translateFileUrl',
})
export class TranslateFileUrlPipe implements PipeTransform {
  transform(value: StorageVM): unknown {
    return value.blobUrl?.replace(
      `https://saobslugaprd01.blob.core.windows.net/storage/`,
      'https://trotamundos.pl/files.php/',
    );
  }
}
