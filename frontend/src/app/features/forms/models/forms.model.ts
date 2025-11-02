import { FormControlModel } from './form-control.model';

export class FormsModel {
  public readonly formId: string = null!;
  public readonly name: string = null!;
  public readonly isActive: boolean = false;

  public readonly controls: FormsModelElement[] = [];

  constructor(init?: Partial<FormsModel>) {
    Object.assign(this, init);
  }
}

export class FormsModelElement {
  formControlId: string = null!;
  formControl: FormControlModel = null!;
  order: number = null!;
}
