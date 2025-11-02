import { inject, Pipe, PipeTransform } from '@angular/core';
import { TranslationService } from '../services/translation.service';

@Pipe({
  name: 'translate',
  standalone: true,
  pure: false, // Mark the pipe as impure to trigger updates
})
export class TranslatePipe implements PipeTransform {
  private readonly _translation = inject(TranslationService);

  async ngOnInit() {
    await this._translation.initialize();
  }

  transform(
    key: string,
    interpolateParams?: { [key: string]: string },
  ): string {
    return this._translation.instant(key, interpolateParams);
  }
}
