import { TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UsersGateway } from './users.gateway';
import { UsersApi } from './users.api';
import { UsersListDto } from '@obsluga-it/users/domain';

describe('UsersGateway', () => {
  let gateway: UsersGateway;
  let mockApi: jest.Mocked<UsersApi>;

  beforeEach(() => {
    mockApi = {
      getUsers: jest.fn(),
    } as unknown as jest.Mocked<UsersApi>;

    TestBed.configureTestingModule({
      providers: [
        UsersGateway,
        { provide: UsersApi, useValue: mockApi },
      ],
    });

    gateway = TestBed.inject(UsersGateway);
  });

  it('should map DTO to domain model', (done) => {
    const dto: UsersListDto = {
      items: [{ id: '1', email: 'test@test.com', display_name: 'Test User', role: 'ADMIN', is_active: true }],
      total: 1,
    };
    mockApi.getUsers.mockReturnValue(of(dto));

    gateway.getUsers().subscribe((result) => {
      expect(result.items[0].displayName).toBe('Test User');
      expect(result.items[0].role).toBe('ADMIN');
      done();
    });
  });

  it('should normalize HTTP errors to domain errors', (done) => {
    const httpError = new HttpErrorResponse({ status: 404, statusText: 'Not Found' });
    mockApi.getUsers.mockReturnValue(throwError(() => httpError));

    gateway.getUsers().subscribe({
      error: (err) => {
        expect(err.code).toBe('NOT_FOUND');
        expect(err).not.toBeInstanceOf(HttpErrorResponse);
        done();
      },
    });
  });

  it('should normalize 401 to UNAUTHORIZED domain error', (done) => {
    const httpError = new HttpErrorResponse({ status: 401 });
    mockApi.getUsers.mockReturnValue(throwError(() => httpError));

    gateway.getUsers().subscribe({
      error: (err) => {
        expect(err.code).toBe('UNAUTHORIZED');
        done();
      },
    });
  });

  it('should normalize network error (status 0) to NETWORK_ERROR', (done) => {
    const httpError = new HttpErrorResponse({ status: 0 });
    mockApi.getUsers.mockReturnValue(throwError(() => httpError));

    gateway.getUsers().subscribe({
      error: (err) => {
        expect(err.code).toBe('NETWORK_ERROR');
        done();
      },
    });
  });
});
