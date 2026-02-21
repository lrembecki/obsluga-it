import { Component, inject } from '@angular/core';
import { UsersStore } from './users.store';
import { UsersTableComponent } from '@obsluga-it/users/ui';
import { User } from '@obsluga-it/users/domain';

@Component({
  selector: 'feature-users-page',
  standalone: true,
  imports: [UsersTableComponent],
  providers: [UsersStore],
  template: `
    @if (store.isLoading()) {
      <p>Loading...</p>
    } @else if (store.error) {
      <p>Error: {{ store.error.message }}</p>
    } @else {
      <users-table
        [users]="store.users()?.items ?? []"
        (userSelected)="onUserSelected($event)"
      />
    }
  `,
})
export class UsersPage {
  protected readonly store = inject(UsersStore);

  protected onUserSelected(_user: User): void {
    // Handle user selection: navigate to detail page or open a dialog
  }
}
