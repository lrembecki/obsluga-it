import { Directive, HostListener, model, output } from '@angular/core';

@Directive({
  selector: '[app-confirm]',
})
export class Confirm {
  public readonly message = model<string>(null!);
  public readonly confirmed = output<Event>();

  @HostListener('click', ['$event'])
  public click($event: Event): void {
    $event.preventDefault();
    $event.stopPropagation();

    if (confirm(this.message()) === true) {
      this.confirmed.emit($event);
    }
  }
}
