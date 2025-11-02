import { Component, inject, Injectable, signal } from '@angular/core';
import { TranslationService } from 'app/core/services/translation.service';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-button-panel',
  imports: [MenubarModule],
  template: ` <p-menubar [model]="data()" /> `,
  styles: `
    .p-menubar {
      margin-bottom: 0.5rem;
    }
  `,
})
export class ButtonPanel {
  private readonly _service = inject(ButtonPanelService);
  protected readonly data = this._service.data.asReadonly();
}

@Injectable()
export class ButtonPanelService {
  protected readonly _translation = inject(TranslationService);
  public readonly data = signal<MenuItem[]>([]);
}
