import { Component, input, output } from '@angular/core';
import { User } from '@obsluga-it/users/domain';
import { TableComponent } from '@obsluga-it/shared/ui-kit';

@Component({
  selector: 'users-table',
  standalone: true,
  imports: [TableComponent],
  template: `
    <ui-table
      [rows]="usersAsRows()"
      [columns]="columns"
      (rowSelected)="onRowSelected($event)"
    />
  `,
})
export class UsersTableComponent {
  readonly users = input.required<User[]>();
  readonly userSelected = output<User>();

  protected readonly columns = [
    { field: 'displayName', header: 'Name' },
    { field: 'email', header: 'Email' },
    { field: 'role', header: 'Role' },
  ];

  protected usersAsRows(): (User & Record<string, unknown>)[] {
    return this.users() as (User & Record<string, unknown>)[];
  }

  protected onRowSelected(row: Record<string, unknown>): void {
    this.userSelected.emit(row as User);
  }
}
