import { Component, inject, OnInit } from '@angular/core';
import { Navbar } from '../navbar/navbar';
import { SeoService, WebsiteFacade } from '@/app/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-page',
  imports: [Navbar, RouterOutlet],
  templateUrl: './page.html',
  styleUrl: './page.scss',
})
export class Page implements OnInit {
  private readonly _seo = inject(SeoService);
  private readonly _facades = {
    website: inject(WebsiteFacade),
  };

  ngOnInit(): void {
    const data = this._facades.website.data();

    this._seo.setTitle(data.title);
    this._seo.setMetaTags({
      description: data.meta.description,
      keywords: data.meta.keywords,
      image: data.meta.image.blobUrl as string,
    });
  }

  onActivate() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
  }
}
