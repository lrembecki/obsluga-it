export interface User {
  readonly id: string;
  readonly email: string;
  readonly displayName: string;
  readonly role: UserRole;
  readonly isActive: boolean;
}

export type UserRole = 'ADMIN' | 'USER' | 'VIEWER';
