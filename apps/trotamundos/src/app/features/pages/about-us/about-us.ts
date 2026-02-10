import { AboutUsFacade } from '@/app/core/facades/about-us.facade';
import { JsonPipe } from '@angular/common';
import { Component, computed, inject } from '@angular/core';

@Component({
  selector: 'app-about-us',
  imports: [JsonPipe],
  templateUrl: './about-us.html',
  styleUrl: './about-us.scss',
})
export class AboutUs {
  private readonly _facade = inject(AboutUsFacade);
  public readonly data = this._facade.data;
  public readonly description = computed(() =>
    this.data()
      .description.split('\n')
      .filter((e) => e.length > 0),
  );
}
