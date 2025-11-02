import { Directive, inject, input } from '@angular/core';
import { IconType } from 'app/core/defaults/icon.default';
import { Button } from '../button/button';

@Directive({
  selector: '[icon]',
})
export class Icon {
  public readonly icon = input<IconType>();
  public readonly tooltip = input.required<string>();

  public implements = {
    button: inject(Button, { self: true, optional: true }),
  };

  ngOnInit() {}
}
