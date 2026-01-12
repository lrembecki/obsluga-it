import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { environment } from 'environments/environment';
import { catchError, lastValueFrom, Observable, of } from 'rxjs';
import { ServiceCallResult } from '../models/service-call-result.model';
import { NotificationInput, NotificationService } from './notification.service';
import { TranslationService } from './translation.service';

export class ApiService {
  private readonly _apiUrl = environment.apiUrl;
  private readonly _translation = inject(TranslationService);
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
      message: `Internal exception`,
      data: null!,
    };

    try {
      result = await lastValueFrom(
        this.getObservable<T>(endpoint, params, headers),
      );
    } catch (e: any) {
      result.message = e.message ?? result.message;
    }

    result.message = this._translation.instant(result.message);

    return result;
  }

  public async delete<T>(endpoint: string): Promise<ServiceCallResult<T>> {
    let result: ServiceCallResult<T> = {
      success: false,
      message: `Internal exception`,
      data: null!,
    };

    try {
      result = await lastValueFrom(this.deleteObservable<T>(endpoint));
    } catch (e: any) {
      result.message = e.message ?? result.message;
    }

    result.message = this._translation.instant(result.message);

    return result;
  }

  public async post<T>(
    endpoint: string,
    model: unknown,
  ): Promise<ServiceCallResult<T>> {
    let result: ServiceCallResult<T> = {
      success: false,
      message: `Internal exception`,
      data: null!,
    };

    try {
      result = await lastValueFrom(this.postObservable<T>(endpoint, model));
    } catch (e: any) {
      result.message = e.message ?? result.message;
    }

    result.message = this._translation.instant(result.message);

    return result;
  }

  public async put<T>(
    endpoint: string,
    model: unknown,
  ): Promise<ServiceCallResult<T>> {
    let result: ServiceCallResult<T> = {
      success: false,
      message: `Internal exception`,
      data: null!,
    };

    try {
      result = await lastValueFrom(this.putObservable<T>(endpoint, model));
    } catch (e: any) {
      result.message = e.message ?? result.message;
    }

    result.message = this._translation.instant(result.message);

    return result;
  }

  private handleException<T>() {
    return catchError<ServiceCallResult<T>, Observable<ServiceCallResult<T>>>(
      (err: HttpErrorResponse) => {
        let input: NotificationInput = null!;

        if (err.status === 0) {
          input = new NotificationInput({
            title: 'ERRORS.SERVICE_NOT_RESPONDING',
            message: 'ERRORS.SERVICE_OFFLINE',
          });
        } else if (err.status === 401) {
          input = new NotificationInput({
            title: 'ERRORS.UNAUTHORIZED',
            message: 'ERRORS.AUTHENTICATION_REQUIRED',
          });
        } else if (err.status === 403) {
          input = new NotificationInput({
            title: 'ERRORS.FORBIDDEN',
            message: 'ERRORS.NO_PERMISSION',
          });
        } else if (err.status === 405) {
          input = new NotificationInput({
            title: 'ERRORS.METHOD_NOT_ALLOWED',
            message: 'ERRORS.METHOD_NOT_IMPLEMENTED',
          });
        } else {
          input = new NotificationInput({
            title: 'ERRORS.UNHANDLED_EXCEPTION',
            message: 'ERRORS.SOMETHING_WENT_WRONG',
          });
        }

        input = new NotificationInput({
          title: this._translation.instant(input.title),
          message: this._translation.instant(input.message),
        });

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
