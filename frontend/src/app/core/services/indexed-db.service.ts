import { Injectable } from '@angular/core';
import {
  ObjectStores,
  ObjectStoreType,
} from '../defaults/object-store.default';

@Injectable()
export class IndexedDbService {
  private readonly dbName = 'obsluga-it';
  private readonly dbVersion = 1;
  private readonly _objectStores: readonly ObjectStoreType[] = ObjectStores;

  private db: IDBDatabase = null!;

  public initialize(): void {
    this.initDatabase();
  }

  private initDatabase() {
    const request = window.indexedDB.open(this.dbName, this.dbVersion);
    request.onupgradeneeded = (event: any) => {
      this.db = event.target.result;

      // Create object stores and set up your database schema here
      this._objectStores.forEach((storeName) => {
        if (!this.db.objectStoreNames.contains(storeName)) {
          this.db.createObjectStore(storeName, {
            keyPath: 'identity',
            autoIncrement: false,
          });
        }
      });
    };
    request.onsuccess = (event: any) => {
      this.db = event.target.result;
      // Database is successfully opened and ready to use
    };
    request.onerror = (event: any) => {
      console.error('Error opening database:', event.target.error);
    };
  }
}
