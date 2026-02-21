import { inject } from '@angular/core';
import { HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { AuthFacade } from '../facades/auth.facade';

const AUTH_ENDPOINT = 'api-endpoint/account/authenticate';

export const internalJwtInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const authFacade = inject(AuthFacade);

  // Never attach internal JWT to the auth endpoint
  if (req.url.includes(AUTH_ENDPOINT)) {
    return next(req);
  }

  const session = authFacade.session();
  if (!session?.internalJwt) {
    return next(req);
  }

  const cloned = req.clone({
    setHeaders: { Authorization: `Bearer ${session.internalJwt}` },
  });
  return next(cloned);
};
