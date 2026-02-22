import { inject } from '@angular/core';
import {
  HttpInterceptorFn,
  HttpRequest,
  HttpHandlerFn,
} from '@angular/common/http';
import { JwtStorageService } from '../services/jwt-storage.service';
import { AUTH_RUNTIME_CONFIG } from '../models/auth-runtime-config.model';

const AUTH_ENDPOINT_PATH = '/api/authenticate';

export const internalJwtInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const jwtStorage = inject(JwtStorageService);
  const authConfig = inject(AUTH_RUNTIME_CONFIG, { optional: true });
  const authenticateEndpoint =
    authConfig?.authenticateEndpoint ?? AUTH_ENDPOINT_PATH;

  // Never attach internal JWT to the auth endpoint
  if (
    req.url.includes(authenticateEndpoint) ||
    req.url.includes(AUTH_ENDPOINT_PATH)
  ) {
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
