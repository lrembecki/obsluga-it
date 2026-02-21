import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthFacade } from '@obsluga-it/core/auth';
import { PermissionType } from './permission.types';

export function authorizedGuard(permissions: PermissionType[]): CanActivateFn {
  return () => {
    const auth = inject(AuthFacade);
    const router = inject(Router);

    const session = auth.session();
    if (!session?.internalJwt) {
      router.navigate(['/login']);
      return false;
    }

    return true;
  };
}
