import { Component, input } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { Message } from 'primeng/message';

@Component({
  selector: 'app-error',
  imports: [Message],
  template: `

    @if (control() && control().touched && control().errors) {
      <p-message severity="error">
        {{ 'Pole zawiera błędy.' }}
      </p-message>
    }
  `,
  styles: ``
})
export class Error {
  control = input<AbstractControl>(null!);
}
