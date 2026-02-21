import { TestBed } from '@angular/core/testing';
import { PLATFORM_ID } from '@angular/core';
import { JwtStorageService } from './jwt-storage.service';
import { AuthSession } from '../models/auth-session.model';

describe('JwtStorageService', () => {
  let service: JwtStorageService;

  describe('in browser environment', () => {
    beforeEach(() => {
      TestBed.configureTestingModule({
        providers: [
          JwtStorageService,
          { provide: PLATFORM_ID, useValue: 'browser' },
        ],
      });
      service = TestBed.inject(JwtStorageService);
      localStorage.clear();
    });

    it('should store and retrieve session', () => {
      const session: AuthSession = { internalJwt: 'test-token', expiresAt: Date.now() + 3600000 };
      service.store(session);
      expect(service.retrieve()).toEqual(session);
    });

    it('should return null when no session stored', () => {
      expect(service.retrieve()).toBeNull();
    });

    it('should clear session', () => {
      const session: AuthSession = { internalJwt: 'test-token', expiresAt: Date.now() + 3600000 };
      service.store(session);
      service.clear();
      expect(service.retrieve()).toBeNull();
    });
  });

  describe('in server environment (SSR guard)', () => {
    beforeEach(() => {
      TestBed.configureTestingModule({
        providers: [
          JwtStorageService,
          { provide: PLATFORM_ID, useValue: 'server' },
        ],
      });
      service = TestBed.inject(JwtStorageService);
    });

    it('should NOT access localStorage on server', () => {
      const localStorageSpy = jest.spyOn(Storage.prototype, 'getItem');
      service.retrieve();
      expect(localStorageSpy).not.toHaveBeenCalled();
    });

    it('should return null on server', () => {
      expect(service.retrieve()).toBeNull();
    });

    it('should NOT store to localStorage on server', () => {
      const session: AuthSession = { internalJwt: 'test-token', expiresAt: Date.now() + 3600000 };
      const setItemSpy = jest.spyOn(Storage.prototype, 'setItem');
      service.store(session);
      expect(setItemSpy).not.toHaveBeenCalled();
    });
  });
});
