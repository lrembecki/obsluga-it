export interface AuthSession {
  readonly internalJwt: string;
  readonly expiresAt: number;
}
