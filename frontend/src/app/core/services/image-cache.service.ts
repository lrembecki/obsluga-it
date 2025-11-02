import { Injectable, signal } from '@angular/core';
import { ImageModel } from 'app/core/models/image.model';

@Injectable({ providedIn: 'root' })
export class ImageCacheService {
  private readonly _preloadedImages = signal(new Set<string>());
  private readonly _imageCache = new Map<string, HTMLImageElement>();

  readonly preloadedImages = this._preloadedImages.asReadonly();

  /**
   * Preload images to improve perceived performance
   */
  preloadImages(images: ImageModel[]): void {
    images.forEach((image) => this.preloadImage(image.blobUrl));
  }

  /**
   * Preload a single image
   */
  preloadImage(url: string): Promise<void> {
    return new Promise((resolve, reject) => {
      if (this._imageCache.has(url)) {
        resolve();
        return;
      }

      const img = new Image();
      img.onload = () => {
        this._imageCache.set(url, img);
        this._preloadedImages.update((set) => new Set([...set, url]));
        resolve();
      };
      img.onerror = () => reject(new Error(`Failed to load image: ${url}`));
      img.src = url;
    });
  }

  /**
   * Check if an image is preloaded
   */
  isPreloaded(url: string): boolean {
    return this._preloadedImages().has(url);
  }

  /**
   * Clear the image cache
   */
  clearCache(): void {
    this._imageCache.clear();
    this._preloadedImages.set(new Set());
  }

  /**
   * Get cache size
   */
  getCacheSize(): number {
    return this._imageCache.size;
  }
}
