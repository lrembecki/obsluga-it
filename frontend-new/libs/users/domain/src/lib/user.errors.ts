import { DomainError } from '@obsluga-it/core/http';

export type UserDomainError =
  | (DomainError & { code: 'NOT_FOUND' })
  | (DomainError & { code: 'FORBIDDEN' })
  | (DomainError & { code: 'VALIDATION_ERROR' })
  | (DomainError & { code: 'SERVER_ERROR' })
  | (DomainError & { code: 'NETWORK_ERROR' })
  | (DomainError & { code: 'UNKNOWN_ERROR' });
