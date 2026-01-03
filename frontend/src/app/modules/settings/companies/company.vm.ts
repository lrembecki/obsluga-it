export class CompanyParameterVM {
  order: number = null!;
  name: string = null!;
  value: string = null!;

  constructor(init?: Partial<CompanyParameterVM>) {
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

export class CompanyVM {
  id: string = null!;
  name: string = null!;
  addressId: string = null!;
  address: AddressVM = new AddressVM();
  parameters: CompanyParameterVM[] = [];
  emailContact: string[] = [];
  phoneContact: string[] = [];

  constructor(init?: Partial<CompanyVM>) {
    Object.assign(this, init);
    this.address = new AddressVM(init?.address);
    this.parameters =
      init?.parameters?.map((param) => new CompanyParameterVM(param)) ?? [];
  }
}
