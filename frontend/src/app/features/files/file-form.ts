import { Component, computed, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { ButtonPanelService } from 'app/shared/ui/button-panel/button-panel';
import { Button } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { DropdownInputComponent } from 'app/shared/ui/inputs/dropdown-input.component';
import { NumberInputComponent } from 'app/shared/ui/inputs/number-input.component';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { LoadingComponent } from 'app/shared/ui/loading/loading.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { GroupFormButton } from '../groups/group-form-button';
import { FileFacade, FileGroupFacade } from './file.facade';
import { FileModel } from './file.model';

@Component({
  selector: 'app-file-form',
  imports: [
    ReactiveFormsModule,
    TextInputComponent,
    TranslatePipe,
    UiPanel,
    DropdownInputComponent,
    Button,
    RouterLink,
    GroupFormButton,
    ButtonDelete,
    LoadingComponent,
    NumberInputComponent,
  ],
  template: `
    <div class="file-form-container">
      @if (loading()) {
        <app-loading [text]="'Loading file data'" />
      }

      @if (_formGroup()) {
        <h1>{{ 'File' | translate }}: {{ _model()!.name }}</h1>

        <app-ui-panel>
          <ng-template #start>
            <app-button
              [text]="'Submit' | translate"
              color="primary"
              [disabled]="!_formGroup()!.valid"
              [isInProgress]="saving()"
              (buttonClick)="submit()"
            />
            <app-group-form-button />
          </ng-template>
          <ng-template #end>
            <app-button
              [text]="'Return to list' | translate"
              [routerLink]="['..', 'list']"
            />
          </ng-template>
        </app-ui-panel>

        <form [formGroup]="_formGroup()!" class="file-form">
          <app-text-input
            formControlName="displayName"
            [label]="'Display name' | translate"
          />
          <app-dropdown-input
            formControlName="fileGroupId"
            [label]="'Group' | translate"
            [data]="_facades.groups.data()"
            valueField="groupId"
            [clearable]="true"
          />
          <app-number-input
            formControlName="position"
            [label]="'Position' | translate"
          />
        </form>

        <app-ui-panel>
          <ng-template #end>
            <app-button
              delete
              [facade]="{
                identity: _model()!.fileId,
                facade: _facades.files,
              }"
              (deleted)="_router.navigate(['/files'])"
            />
          </ng-template>
        </app-ui-panel>

        <h3>Sample</h3>

        <a [href]="_model()?.url" target="_blank">{{
          _model()?.displayName ?? _model()?.name
        }}</a>
      }
    </div>
  `,
  styles: `
    .file-form-container {
      position: relative;
      min-height: 200px;
    }

    .file-form {
      margin-bottom: 0.5rem;
    }
  `,
  providers: [ButtonPanelService],
})
export class FileForm {
  private readonly _builder = inject(FormBuilder);
  private readonly _route = inject(ActivatedRoute);
  protected readonly _router = inject(Router);
  protected readonly _facades = {
    files: inject(FileFacade),
    groups: inject(FileGroupFacade),
  };

  private readonly routeParams = toSignal(this._route.params);
  protected _model = computed(() =>
    this._facades.files
      .data()
      .find((e) => e.fileId === this.routeParams()!['fileId']),
  );

  protected loading = computed(() =>
    Object.values(this._facades).some((e) => e.loading()),
  );
  protected saving = signal(false);

  protected _formGroup = computed(() =>
    !this._model()
      ? null
      : this._builder.group({
          fileId: [this._model()!.fileId, [Validators.required]],
          name: [this._model()!.name, [Validators.required]],
          displayName: [this._model()!.displayName, [Validators.required]],
          fileGroupId: [
            this._model()!.fileGroupId,
            [Validators.required, Validators.min(1)],
          ],
          position: [this._model()!.position],
        }),
  );

  protected async submit(): Promise<void> {
    const model = new FileModel(this._formGroup()?.value as Partial<FileModel>);

    this.saving.set(true);
    const response = await this._facades.files.update(model.fileId, model);
    this.saving.set(false);

    if (response.success) {
      this._router.navigate(['/files']);
    }
  }

  protected async removeObject() {
    alert('delete');
  }
}
