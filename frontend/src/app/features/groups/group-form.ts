import { Component, computed, inject, input, model } from '@angular/core';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { UiDialogForm } from 'app/shared/ui/ui-dialog-form';
import { GroupFacade } from './group.facade';
import { GroupModel } from './group.model';

@Component({
  selector: 'app-group-form',
  imports: [TextInputComponent, TranslatePipe],
  template: `
    <app-text-input
      [(value)]="groupModel().name"
      (valueChange)="update()"
      [label]="'Name' | translate"
    />
  `,
  styles: ``,
})
export class GroupForm {
  public readonly dialog = input.required<UiDialogForm>();
  public readonly groupModel = model<GroupModel>(new GroupModel());
  public readonly isValid = computed(() => this.groupModel().name?.length > 0);
  private readonly _groups = inject(GroupFacade);

  ngOnInit() {
    this.dialog().submit.subscribe(this.save.bind(this));
  }

  public async save(): Promise<void> {
    const model = this.groupModel();

    this.dialog().saving.set(true);

    const response = await this._groups.create(null!, model);
    this.dialog().saving.set(false);

    if (response.success) {
      this.groupModel.set(new GroupModel());
      this.dialog().close();
    }
  }

  protected update(): void {
    this.groupModel.set(new GroupModel(this.groupModel()));
  }
}
