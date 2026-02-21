import { inject, Injectable } from '@angular/core';
import { Observable, catchError, map, throwError } from 'rxjs';
import { normalizeHttpError } from '@obsluga-it/core/http';
import { User, UsersListDto, mapUserDtoToModel } from '@obsluga-it/users/domain';
import { UsersApi } from './users.api';

export interface UsersList {
  readonly items: User[];
  readonly total: number;
}

@Injectable({ providedIn: 'root' })
export class UsersGateway {
  private readonly api = inject(UsersApi);

  getUsers(): Observable<UsersList> {
    return this.api.getUsers().pipe(
      map((dto: UsersListDto) => ({
        items: dto.items.map(mapUserDtoToModel),
        total: dto.total,
      })),
      catchError((err) => throwError(() => normalizeHttpError(err)))
    );
  }
}
