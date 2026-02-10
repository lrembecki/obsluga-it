export class FileStorageVM {
  displayName: string = null!;
  description: string = null!;

  constructor(init?: Partial<FileStorageVM>) {
    Object.assign(this, init);
  }
}

export class ImageStorageVM {
  width: number = null!;
  height: number = null!;

  constructor(init?: Partial<ImageStorageVM>) {
    Object.assign(this, init);
  }
}

export class StorageVM {
  id: string = null!; // Guid
  binaryData?: string | null;
  filename?: string | null;
  blobUrl?: string | null;
  blobPath?: string | null;
  size?: number | null;
  file?: FileStorageVM | null;
  image?: ImageStorageVM | null;

  constructor(init?: Partial<StorageVM>) {
    Object.assign(this, init);

    if (this.blobUrl) {
      this.blobUrl = this.blobUrl.replace(
        `https://saobslugaprd01.blob.core.windows.net/storage/`,
        'https://trotamundos.pl/files.php/',
      );
    }
  }
}
