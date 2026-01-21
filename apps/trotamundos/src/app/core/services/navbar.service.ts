import { signal } from '@angular/core';
import { MenuItemModel } from '../models/menu-item';

export class NavbarService {
  menuItems = signal<MenuItemModel[]>([
    { label: 'O nas', routerLink: ['/o-nas'] },
    { label: 'Jak to działa', routerLink: ['/jak-to-dziala'] },
    { label: 'Indywidualne', routerLink: ['/indywidualne'] },
    { label: 'Grupowe', routerLink: ['/grupowe'] },
    { label: 'Kontakt', routerLink: ['/kontakt'] },
  ]);
}
