import { ImageStorageVM, StorageVM } from "./storage.vm";

export function convertImageFileToStorageVM(file: File): Promise<StorageVM> {
    return new Promise<StorageVM>((resolve, reject) => {
        try {
            const reader = new FileReader();

            reader.onload = () => {
                const result = reader.result as string;

                const image = new Image();
                image.onload = () => {
                    const storageVM: StorageVM = new StorageVM({
                        binaryData: result,
                        filename: file.name,
                        size: file.size,
                        image: new ImageStorageVM({
                            width: image.width,
                            height: image.height
                        })
                    });

                    resolve(storageVM);
                };
                image.src = URL.createObjectURL(file);
            }

            reader.readAsDataURL(file);
        } catch (e) {
            reject(e)
        }
    });
}