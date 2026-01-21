import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiUrlScope } from '../defaults/base-storage-url';

@Injectable()
export class ApiService {
  private readonly _endoint = inject(BaseApiUrlScope);
  private readonly _http = inject(HttpClient);
  public get<T>(endpoint: string): Observable<T> {
    return this._http.get<T>(`${this._endoint}/${endpoint}`);
  }
}
