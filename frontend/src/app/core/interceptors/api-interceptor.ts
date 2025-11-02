import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { StorageService } from '../services/storage.service';

export const apiInterceptor: HttpInterceptorFn = (req, next) => {
  const storage = inject(StorageService);

  const { b2cToken, account } = {
    b2cToken: storage.b2cToken.data(),
    account: storage.account.data(),
  };

  const token = req.url.endsWith(`/authenticate`) ? b2cToken : account!.token;

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  return next(req);
};
