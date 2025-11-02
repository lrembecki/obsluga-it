import { Component, inject } from '@angular/core';
import { MenubarModule } from 'primeng/menubar';
import { ButtonPanel, ButtonPanelService } from '../button-panel/button-panel';
import { Table } from '../table/table';
import { TableService } from '../table/table.service';

@Component({
  selector: 'app-list',
  imports: [Table, MenubarModule, ButtonPanel],
  template: `
    <div class="list-navbar">
      @if (_buttonPanel && _buttonPanel.data().length) {
        <app-button-panel />
      }
    </div>

    @if (_table) {
      <app-table />
    }
  `,
  styles: `
    .list-navbar {
      display: flex;
      gap: 0.5rem;
      margin-bottom: 0.5rem;

      & app-upload {
        margin-top: 0.3rem;
      }

      & app-button-panel {
        flex: auto;
      }

      & > * {
        margin-bottom: auto;
      }
    }
  `,
})
export class List {
  protected readonly _buttonPanel = inject(ButtonPanelService, {
    optional: true,
  });
  protected readonly _table = inject(TableService, { optional: true });
}
