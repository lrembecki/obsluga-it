export class ImageModel {
  public imageId: string = null!;
  public filename: string = null!;
  public blobUrl: string = null!;
  public blobPath: string = null!;
  public width: number = null!;
  public height: number = null!;
  public tags: string[] = [];
  public description: string = null!;
  public displayName: string = null!;

  constructor(init?: Partial<ImageModel>) {
    Object.assign(this, init);
  }
}
