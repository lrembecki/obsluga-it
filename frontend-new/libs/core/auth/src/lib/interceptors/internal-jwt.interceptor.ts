import { inject } from '@angular/core';
import { HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { JwtStorageService } from '../services/jwt-storage.service';

const AUTH_ENDPOINT = 'api-endpoint/account/authenticate';

export const internalJwtInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const jwtStorage = inject(JwtStorageService);

  // Never attach internal JWT to the auth endpoint
  if (req.url.includes(AUTH_ENDPOINT)) {
    return next(req);
  }

  const session = jwtStorage.retrieve();
  if (!session?.internalJwt) {
    return next(req);
  }

  const cloned = req.clone({
    setHeaders: { Authorization: `Bearer ${session.internalJwt}` },
  });
  return next(cloned);
};
