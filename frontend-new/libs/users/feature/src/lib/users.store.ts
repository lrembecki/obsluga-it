import { Injectable, inject } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { UsersGateway, UsersList } from '@obsluga-it/users/data-access';
import { DomainError } from '@obsluga-it/core/http';

@Injectable()
export class UsersStore {
  private readonly usersGateway = inject(UsersGateway);

  readonly usersResource = rxResource<UsersList, void>({
    stream: () => this.usersGateway.getUsers(),
  });

  get isLoading() {
    return this.usersResource.isLoading;
  }

  get users() {
    return this.usersResource.value;
  }

  get error(): DomainError | null {
    const err = this.usersResource.error();
    if (!err) return null;
    return err as unknown as DomainError;
  }

  reload(): void {
    this.usersResource.reload();
  }
}
