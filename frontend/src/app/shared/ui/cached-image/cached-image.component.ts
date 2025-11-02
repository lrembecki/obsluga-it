import { NgOptimizedImage } from '@angular/common';
import { Component, computed, input } from '@angular/core';

@Component({
  selector: 'app-cached-image',
  standalone: true,
  imports: [NgOptimizedImage],
  template: `
    <img
      [ngSrc]="src()"
      [alt]="alt()"
      [width]="width()"
      [height]="height()"
      [placeholder]="placeholder()"
      [priority]="true"
      [sizes]="sizes()"
    />
  `,
  styles: [
    `
      img {
        max-width: 100%;
        height: auto;
        display: block;
      }
    `,
  ],
})
export class CachedImageComponent {
  // Required inputs
  src = input.required<string>();
  alt = input.required<string>();
  width = input.required<number>();
  height = input.required<number>();

  // Optional inputs with defaults
  placeholder = input<'blur' | 'empty'>('empty');
  priority = input<boolean>(false);
  loading = input<'lazy' | 'eager'>('lazy');
  sizes = input<string>('100vw');

  // Computed values for optimization
  aspectRatio = computed(() => {
    const w = this.width();
    const h = this.height();
    return h > 0 ? w / h : 1;
  });
}
