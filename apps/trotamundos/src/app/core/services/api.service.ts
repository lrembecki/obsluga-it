import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable()
export class ApiService {
  private readonly _http = inject(HttpClient);
}
