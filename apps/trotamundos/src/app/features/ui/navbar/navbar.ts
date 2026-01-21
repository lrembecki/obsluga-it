import { NavbarService } from '@/app/core/services/navbar.service';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LogoComponent } from '../logo/logo.component';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, LogoComponent],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {
  private readonly _navbar = inject(NavbarService);
  protected readonly menuItems = this._navbar.menuItems.asReadonly();
}
