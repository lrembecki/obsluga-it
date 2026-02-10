import { NavbarService } from '@/app/core/services/navbar.service';
import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LogoComponent } from '../logo/logo.component';
import { SidenavComponent } from '../sidenav/sidenav';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, LogoComponent, SidenavComponent],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {
  private readonly _navbar = inject(NavbarService);
  protected readonly menuItems = this._navbar.menuItems.asReadonly();
  protected readonly isSidenavOpen = signal(false);

  protected openSidenav(): void {
    this.isSidenavOpen.set(true);
  }

  protected closeSidenav(): void {
    this.isSidenavOpen.set(false);
  }
}
