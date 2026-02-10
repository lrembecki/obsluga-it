import { MenuItemModel } from '@/app/core/models/menu-item';
import { Component, input, output } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidenav',
  imports: [RouterLink],
  templateUrl: './sidenav.html',
  styleUrl: './sidenav.scss',
})
export class SidenavComponent {
  menuItems = input.required<MenuItemModel[]>();
  close = output<void>();

  protected onClose(): void {
    this.close.emit();
  }

  protected onMenuItemClick(): void {
    this.close.emit();
  }
}
