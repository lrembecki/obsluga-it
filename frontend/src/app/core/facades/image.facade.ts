import { Injectable, inject, effect } from '@angular/core';
import { ApiFacade } from '../interfaces/facade.interface';
import { ImageModel } from '../models/image.model';
import { ImageCacheService } from '../services/image-cache.service';

@Injectable({ providedIn: 'root' })
export class ImageFacade extends ApiFacade<ImageModel[]> {
  private readonly _imageCache = inject(ImageCacheService);

  constructor() {
    super([], 'images');

    // Preload images when data changes
    effect(() => {
      const images = this.data();
      if (images.length > 0) {
        // Preload first 5 images for better performance
        this._imageCache.preloadImages(images.slice(0, 5));
      }
    });
  }

  /**
   * Preload all images in the collection
   */
  preloadAllImages(): void {
    this._imageCache.preloadImages(this.data());
  }

  /**
   * Check if an image is cached
   */
  isImageCached(url: string): boolean {
    return this._imageCache.isPreloaded(url);
  }
}
