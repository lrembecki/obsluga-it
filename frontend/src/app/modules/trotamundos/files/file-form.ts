import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { cachedComputed } from 'app/core/helpers/signal.helper';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { UploadFileInput } from 'app/shared/storage/upload-file-input';
import { Button } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { NumberInputComponent } from 'app/shared/ui/inputs/number-input.component';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectTrotamundosFiles } from './file.provider';
import { FileVM } from './file.vm';

@Component({
    selector: 'app-file-form',
    hostDirectives: [
        {
            directive: Valid,
            outputs: ['isValidChange'],
        },
    ],
    imports: [
        UiPanel,
        Button,
        RouterLink,
        TranslatePipe,
        TextInputComponent,
        UploadFileInput,
        NumberInputComponent,
        Valid,
        Required,
        ButtonSubmit,
        ButtonReturn,
        ButtonDelete,
    ],
    template: `
    @if (model.session()) {
      <ng-container validate #validate="validate">
        <h1>{{ 'TROTAMUNDOS.FILES' | translate }}</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.files.saving()"
            />
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>

        <app-text-input
          [disabled]="!!model.session().id"
          [(value)]="model.session().group"
          [required]="true"
          label="Group"
        />

        <app-text-input
          [(value)]="model.session().displayName"
          [required]="true"
          label="Display Name"
        />

        <app-text-input
          [(value)]="model.session().description"
          label="Description"
        />

        <app-upload-file-input
          [(value)]="model.session().storage"
        />

        <app-number-input
          [(value)]="model.session().position"
          label="Position"
        />

        @if (model.session().storage.id) {
            <a [href]="model.session().storage.blobUrl" target="_blank" >{{ model.session().displayName }}</a>
        }

        @if (model.session().id) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{
                  identity: model.session().id,
                  facade: _services.files,
                }"
                (deleted)="returnToList()"
              />
            </ng-template>
          </app-ui-panel>
        }
      </ng-container>
    }
  `,
    styles: `
    :host {
        display: flex;
        flex-direction: column;
        gap: 1rem;
    }
  `,
})
export class FileForm {
    protected readonly _services = injectTrotamundosFiles();
    protected readonly routeParams = toSignal(
        this._services.activatedRoute.params,
    );
    protected readonly fileId = computed(
        () => this.routeParams()!['id'] as string,
    );
    protected readonly model = cachedComputed(
        () =>
            this._services.files
                .data()
                .find((e) => e.id === this.fileId()) ?? new FileVM(),
        (entry) => new FileVM(entry),
    );

    protected async submit(): Promise<void> {
        const session = this.model.session();
        const response = session.id
            ? await this._services.files.update(session.id, session)
            : await this._services.files.create('', session);

        if (response.success) {
            this.returnToList();
        }
    }

    protected returnToList() {
        this._services.router.navigate(['/modules/trotamundos/files']);
    }
}
