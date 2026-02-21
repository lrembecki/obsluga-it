export class CompanyVM {
  id: string = null!;
  name: string = null!;
  address: AddressVM = new AddressVM();
  parameters: CompanyParameterVM[] = [];

  constructor(init?: Partial<CompanyVM>) {
    Object.assign(this, init);
  }
}

export class AddressVM {
  id: string = null!;
  address1: string = null!;
  address2: string = null!;
  city: string = null!;
  postalCode: string = null!;
  country: string = null!;

  constructor(init?: Partial<AddressVM>) {
    Object.assign(this, init);
  }
}

export class CompanyParameterVM {
  order: number = 0;
  name: string = null!;
  value: string = null!;

  constructor(init?: Partial<CompanyParameterVM>) {
    Object.assign(this, init);
  }
}
