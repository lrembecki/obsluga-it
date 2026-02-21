import { TestBed } from '@angular/core/testing';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { signal } from '@angular/core';
import { internalJwtInterceptor } from './internal-jwt.interceptor';
import { AuthFacade } from '../facades/auth.facade';
import { AuthSession } from '../models/auth-session.model';

describe('internalJwtInterceptor', () => {
  let http: HttpClient;
  let httpMock: HttpTestingController;
  let mockSession: ReturnType<typeof signal<AuthSession | null>>;

  beforeEach(() => {
    mockSession = signal<AuthSession | null>(null);

    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(withInterceptors([internalJwtInterceptor])),
        provideHttpClientTesting(),
        {
          provide: AuthFacade,
          useValue: { session: mockSession },
        },
      ],
    });

    http = TestBed.inject(HttpClient);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('should NOT attach JWT to auth endpoint', () => {
    mockSession.set({ internalJwt: 'internal-jwt', expiresAt: Date.now() + 3600000 });

    http.post('api-endpoint/account/authenticate', {}).subscribe();
    const req = httpMock.expectOne('api-endpoint/account/authenticate');
    expect(req.request.headers.has('Authorization')).toBe(false);
    req.flush({});
  });

  it('should attach internal JWT to business API requests', () => {
    mockSession.set({ internalJwt: 'internal-jwt', expiresAt: Date.now() + 3600000 });

    http.get('/api/users').subscribe();
    const req = httpMock.expectOne('/api/users');
    expect(req.request.headers.get('Authorization')).toBe('Bearer internal-jwt');
    req.flush([]);
  });

  it('should NOT attach any token when no session', () => {
    mockSession.set(null);

    http.get('/api/users').subscribe();
    const req = httpMock.expectOne('/api/users');
    expect(req.request.headers.has('Authorization')).toBe(false);
    req.flush([]);
  });
});
