import { Component, HostBinding, input, model } from '@angular/core';

@Component({
  selector: 'app-logo',
  standalone: true,
  imports: [],
  templateUrl: './logo.component.html',
  styleUrl: './logo.component.scss'
})
export class LogoComponent {
  color = model<'white' | 'transparent'>('white');

  @HostBinding('style.background-color')
  get backgroundColor() {
    return this.color();
  }
}
