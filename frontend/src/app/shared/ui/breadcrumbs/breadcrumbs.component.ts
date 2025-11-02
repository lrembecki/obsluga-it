import { Component, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { BreadcrumbsService } from 'app/core/services/breadcrumbs.service';
import { TranslationService } from 'app/core/services/translation.service';
import { MenuItem } from 'primeng/api';
import { Breadcrumb } from 'primeng/breadcrumb';

@Component({
  imports: [Breadcrumb],
  selector: 'app-breadcrumbs',
  template: ` <p-breadcrumb [model]="items()" [home]="dashboard" /> `,
  styles: [
    `
      .breadcrumb {
        background: transparent;
        padding: 0;
        margin: 0;
      }
    `,
  ],
})
export class BreadcrumbsComponent {
  private readonly _router = inject(Router);
  private readonly _breadcrumbs = inject(BreadcrumbsService);
  private readonly _translation = inject(TranslationService);

  protected readonly items = computed(() => {
    const data = this._breadcrumbs.data().map((e) => ({
      ...e,
      label: this._translation.instant(e.label!),
    }));

    data.forEach((e, index) => {
      if (index !== 0) {
        e.routerLink = `${data[index - 1].routerLink}/${e.routerLink}`;
      }
    });

    return data;
  });

  protected readonly dashboard: MenuItem = {
    label: this._translation.instant('DASHBOARD.NAME'),
    routerLink: '/',
  };

  ngOnInit() {}

  navigateTo(url: string): void {
    this._router.navigateByUrl(url);
  }
}
