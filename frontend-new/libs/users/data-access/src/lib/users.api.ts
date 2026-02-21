import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UsersListDto } from '@obsluga-it/users/domain';

@Injectable({ providedIn: 'root' })
export class UsersApi {
  private readonly http = inject(HttpClient);

  getUsers(): Observable<UsersListDto> {
    return this.http.get<UsersListDto>('/api/users');
  }
}
