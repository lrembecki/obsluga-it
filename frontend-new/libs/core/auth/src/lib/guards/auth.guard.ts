import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthFacade } from '../facades/auth.facade';

export const authGuard: CanActivateFn = () => {
  const authFacade = inject(AuthFacade);
  const router = inject(Router);

  const session = authFacade.session();
  if (session?.internalJwt) {
    return true;
  }
  return router.createUrlTree(['/login']);
};
