import { Directive, Injector, OnDestroy, OnInit, inject } from '@angular/core';
import {
  ControlValueAccessor,
  FormControl,
  FormControlDirective,
  FormControlName,
  NgControl,
  NG_VALUE_ACCESSOR,
} from '@angular/forms';
import { Subscription } from 'rxjs';
import { ControlContainer, FormGroup } from '@angular/forms';

@Directive({
  standalone: true,
  selector: '[appHostControl]',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: HostControlDirective,
    },
  ],
})
export class HostControlDirective
  implements ControlValueAccessor, OnInit, OnDestroy
{
  control: FormControl = new FormControl();

  private injector = inject(Injector);
  private subscription?: Subscription;

  ngOnInit() {
    const ngControl = this.injector.get(NgControl, null, {
      self: true,
      optional: true,
    });

    if (ngControl instanceof FormControlDirective) {
      this.control = ngControl.control;
    } else if (ngControl instanceof FormControlName && ngControl.name) {
      const container = this.injector.get(ControlContainer)
        .control as FormGroup;
      this.control = container.controls[ngControl.name] as FormControl;
    } else {
      this.control = new FormControl();
    }
  }

  writeValue() {}
  registerOnChange() {}
  registerOnTouched() {}

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
