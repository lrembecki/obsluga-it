import { inject } from '@angular/core';
import { PermissionType } from '../defaults/permission.default';
import { AuthService } from '../services/auth.service';

export function authorizedGuard(permissions: PermissionType[]): () => boolean {
  return () => {
    const authService = inject(AuthService);
    return authService.isAuthenticated() && authService.isInRole(permissions);
  };
}
