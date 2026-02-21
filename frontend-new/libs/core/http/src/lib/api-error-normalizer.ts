import { HttpErrorResponse } from '@angular/common/http';
import { DomainError, DomainErrorCode } from './domain-error.types';

export function normalizeHttpError(error: unknown): DomainError {
  if (!(error instanceof HttpErrorResponse)) {
    return {
      code: 'UNKNOWN_ERROR',
      message: error instanceof Error ? error.message : 'An unknown error occurred',
    };
  }

  if (error.status === 0) {
    return { code: 'NETWORK_ERROR', message: 'Network connection failed' };
  }

  const code = statusToCode(error.status);
  const message = extractMessage(error);

  return { code, message, details: sanitizeDetails(error.error) };
}

function statusToCode(status: number): DomainErrorCode {
  switch (status) {
    case 401: return 'UNAUTHORIZED';
    case 403: return 'FORBIDDEN';
    case 404: return 'NOT_FOUND';
    case 422: return 'VALIDATION_ERROR';
    default: return status >= 500 ? 'SERVER_ERROR' : 'UNKNOWN_ERROR';
  }
}

function extractMessage(error: HttpErrorResponse): string {
  if (typeof error.error === 'object' && error.error !== null) {
    const e = error.error as Record<string, unknown>;
    if (typeof e['message'] === 'string') return e['message'];
  }
  return error.message;
}

function sanitizeDetails(payload: unknown): unknown {
  if (payload === null || payload === undefined) return undefined;
  if (typeof payload === 'string') return undefined; // avoid leaking raw text
  if (typeof payload === 'object') {
    const { message, errors } = payload as Record<string, unknown>;
    return { message, errors };
  }
  return undefined;
}
