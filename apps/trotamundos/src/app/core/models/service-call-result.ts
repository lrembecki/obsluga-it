export interface ServiceCallResult<T> {
  success: boolean;
  data?: T;
  errorMessage?: string;
}
