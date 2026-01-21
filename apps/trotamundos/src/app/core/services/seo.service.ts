import { inject } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

export class SeoService {
  private readonly _meta = inject(Meta);
  private readonly _title = inject(Title);

  setTitle(title: string) {
    this._title.setTitle(title);
    this._meta.updateTag({ property: 'og:title', content: title });
    this._meta.updateTag({ name: 'twitter:title', content: title });
  }

  setMetaTags(config: { description?: string; keywords?: string; image?: string; url?: string }) {
    if (config.description) {
      this._meta.updateTag({ name: 'description', content: config.description });
    }

    if (config.keywords) {
      this._meta.updateTag({ name: 'keywords', content: config.keywords });
    }

    // OpenGraph
    if (config.image) {
      this._meta.updateTag({ property: 'og:image', content: config.image });
      this._meta.updateTag({ name: 'twitter:image', content: config.image });
    }

    if (config.url) {
      this._meta.updateTag({ property: 'og:url', content: config.url });
      this._meta.updateTag({ name: 'twitter:url', content: config.url });
    }
  }
}
