import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable, InjectionToken } from '@angular/core';
import { catchError, lastValueFrom, Observable, of } from 'rxjs';
import { NotificationService, NotificationInput } from '@obsluga-it/core/notification';
import { ServiceCallResult } from './service-call-result.model';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL', {
  providedIn: 'root',
  factory: () => '/api',
});

@Injectable({ providedIn: 'root' })
export class ApiService {
  private readonly _apiUrl = inject(API_BASE_URL);
  private readonly _http = inject(HttpClient);
  private readonly _message = inject(NotificationService);

  public getObservable<T>(
    endpoint: string,
    params: { [key: string]: string } = {},
    headers: Record<string, string> = {},
  ): Observable<ServiceCallResult<T>> {
    return this._http
      .get<ServiceCallResult<T>>(`${this._apiUrl}/${endpoint}`, {
        params,
        headers: structuredClone(headers),
      })
      .pipe(this.handleException());
  }

  public postObservable<T>(
    endpoint: string,
    model: unknown,
  ): Observable<ServiceCallResult<T>> {
    return this._http
      .post<ServiceCallResult<T>>(`${this._apiUrl}/${endpoint}`, model)
      .pipe(this.handleException());
  }

  public deleteObservable<T>(
    endpoint: string,
  ): Observable<ServiceCallResult<T>> {
    return this._http
      .delete<ServiceCallResult<T>>(`${this._apiUrl}/${endpoint}`)
      .pipe(this.handleException());
  }

  public putObservable<T>(
    endpoint: string,
    model: unknown,
  ): Observable<ServiceCallResult<T>> {
    return this._http
      .put<ServiceCallResult<T>>(`${this._apiUrl}/${endpoint}`, model)
      .pipe(this.handleException());
  }

  public async get<T>(
    endpoint: string,
    params: { [key: string]: string } = {},
    headers: Record<string, string> = {},
  ): Promise<ServiceCallResult<T>> {
    let result: ServiceCallResult<T> = {
      success: false,
      message: 'Internal exception',
      data: undefined,
    };

    try {
      result = await lastValueFrom(this.getObservable<T>(endpoint, params, headers));
    } catch (e: any) {
      result.message = e.message ?? result.message;
    }

    return result;
  }

  public async delete<T>(endpoint: string): Promise<ServiceCallResult<T>> {
    let result: ServiceCallResult<T> = {
      success: false,
      message: 'Internal exception',
      data: undefined,
    };

    try {
      result = await lastValueFrom(this.deleteObservable<T>(endpoint));
    } catch (e: any) {
      result.message = e.message ?? result.message;
    }

    return result;
  }

  public async post<T>(
    endpoint: string,
    model: unknown,
  ): Promise<ServiceCallResult<T>> {
    let result: ServiceCallResult<T> = {
      success: false,
      message: 'Internal exception',
      data: undefined,
    };

    try {
      result = await lastValueFrom(this.postObservable<T>(endpoint, model));
    } catch (e: any) {
      result.message = e.message ?? result.message;
    }

    return result;
  }

  public async put<T>(
    endpoint: string,
    model: unknown,
  ): Promise<ServiceCallResult<T>> {
    let result: ServiceCallResult<T> = {
      success: false,
      message: 'Internal exception',
      data: undefined,
    };

    try {
      result = await lastValueFrom(this.putObservable<T>(endpoint, model));
    } catch (e: any) {
      result.message = e.message ?? result.message;
    }

    return result;
  }

  private handleException<T>() {
    return catchError<ServiceCallResult<T>, Observable<ServiceCallResult<T>>>(
      (err: HttpErrorResponse) => {
        let input: NotificationInput;

        if (err.status === 0) {
          input = new NotificationInput({
            title: 'Service not responding',
            message: 'Service is offline',
          });
        } else if (err.status === 401) {
          input = new NotificationInput({
            title: 'Unauthorized',
            message: 'Authentication required',
          });
        } else if (err.status === 403) {
          input = new NotificationInput({
            title: 'Forbidden',
            message: 'No permission',
          });
        } else if (err.status === 405) {
          input = new NotificationInput({
            title: 'Method not allowed',
            message: 'Method not implemented',
          });
        } else {
          input = new NotificationInput({
            title: 'Error',
            message: 'Something went wrong',
          });
        }

        this._message.error(input);

        return of(
          new ServiceCallResult<T>({
            message: input.title,
          }),
        );
      },
    );
  }
}
