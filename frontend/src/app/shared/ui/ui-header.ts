import { Component, contentChildren, effect, input, model } from '@angular/core';

@Component({
  selector: 'app-ui-header',
  imports: [],
  template: `
   @if (level() === 1) {
    <h1>{{ text() }}</h1>
   } @else if (level() === 2) {
    <h2>{{ text() }}</h2>
   } @else if (level() === 3) {
    <h3>{{ text() }}</h3>
   } @else if (level() === 4) {
    <h4>{{ text() }}</h4>
   } @else if (level() === 5) {
    <h5>{{ text() }}</h5>
   } @else {
    <h6>{{ text() }}</h6>
   }

   <ng-content />
  `,
  styles: ``
})
export class UiHeader {
  text = input.required<string>();
  level = model<number>(1);

  children = contentChildren<UiHeader>(UiHeader);

  constructor() {
    effect(() => this.children().forEach(child => {
      if (child.level() <= this.level()) {
        child.level.set(this.level() + 1);
      }
    }))
  }
}
