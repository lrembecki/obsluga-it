import { Component, inject, model } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageFacade } from 'app/core/facades/image.facade';
import { ImageModel } from 'app/core/models/image.model';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { TranslationService } from 'app/core/services/translation.service';
import {
  ButtonPanel,
  ButtonPanelService,
} from '../../shared/ui/button-panel/button-panel';
import { CachedImageComponent } from '../../shared/ui/cached-image/cached-image.component';
import { TextInputComponent } from '../../shared/ui/inputs/text-input.component';
import { FileModel } from '../files/file.model';

@Component({
  selector: 'app-image-form',
  imports: [
    ReactiveFormsModule,
    TextInputComponent,
    TranslatePipe,
    ButtonPanel,
    CachedImageComponent,
  ],
  template: `
    @if (_formGroup) {
      <h1>{{ 'Image' | translate }}: {{ _model()!.filename }}</h1>

      @let image = _model()!;

      <div style="display: flex; width: 100%; justify-content: center">
        <app-cached-image
          [src]="image.blobUrl"
          [alt]="image.filename"
          [width]="image.width || 350"
          [height]="image.height || 100"
          loading="lazy"
          [priority]="true"
        />
      </div>

      <form [formGroup]="_formGroup" class="file-form">
        <app-text-input
          formControlName="displayName"
          [label]="'Display name' | translate"
        />
      </form>

      <app-button-panel />
    }
  `,
  styles: `
    ::ng-deep app-file-form input {
      width: 100%;
    }

    .file-form {
      margin-bottom: 0.5rem;
    }
  `,
  providers: [ButtonPanelService],
})
export class ImageForm {
  private readonly _formBuilder = inject(FormBuilder);
  private readonly _route = inject(ActivatedRoute);
  private readonly _router = inject(Router);
  private readonly _buttonPanel = inject(ButtonPanelService);
  private readonly _translations = inject(TranslationService);
  private readonly _facade = inject(ImageFacade);

  protected _formGroup: FormGroup = null!;
  protected _model = model<ImageModel>();

  ngOnInit(): void {
    this._buttonPanel.data.set([
      {
        label: this._translations.instant('Save'),
        command: async (event) => {
          if (!this._formGroup.valid) return;
          event.item!.disabled = true;

          const value = this._formGroup.value;
          const response = await this._facade.update(
            `/${this._model()!.imageId}`,
            value,
          );

          event.item!.disabled = false;

          if (response.success) {
            this._router.navigate(['trotamundos/images']);
          }
        },
      },
      {
        label: this._translations.instant('Delete'),
        command: async (event) => {
          event.item!.disabled = true;

          const response = await this._facade.delete(
            `/${this._model()!.imageId}`,
          );

          event.item!.disabled = false;

          if (response.success) {
            this._router.navigate(['trotamundos/images']);
          }
        },
      },
    ]);

    this._route.data.subscribe((data) => {
      this._model.set(data['model']);
      this._formGroup = this.createFormGroup(data['model']);
    });
  }

  private createFormGroup(model: FileModel): FormGroup {
    return this._formBuilder.group({
      displayName: new FormControl(model.displayName, [Validators.required]),
    });
  }
}
