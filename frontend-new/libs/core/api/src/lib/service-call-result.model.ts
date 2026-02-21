export class ServiceCallResult<T> {
  data?: T;
  success: boolean = false;
  message: string = null!;

  constructor(init?: Partial<ServiceCallResult<T>>) {
    Object.assign(this, init);
  }
}
