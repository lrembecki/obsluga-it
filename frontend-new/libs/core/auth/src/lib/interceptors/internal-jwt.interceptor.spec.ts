import { TestBed } from '@angular/core/testing';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { internalJwtInterceptor } from './internal-jwt.interceptor';
import { JwtStorageService } from '../services/jwt-storage.service';
import { AuthSession } from '../models/auth-session.model';

describe('internalJwtInterceptor', () => {
  let http: HttpClient;
  let httpMock: HttpTestingController;
  let storedSession: AuthSession | null;

  beforeEach(() => {
    storedSession = null;

    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(withInterceptors([internalJwtInterceptor])),
        provideHttpClientTesting(),
        {
          provide: JwtStorageService,
          useValue: { retrieve: () => storedSession },
        },
      ],
    });

    http = TestBed.inject(HttpClient);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('should NOT attach JWT to auth endpoint', () => {
    storedSession = { internalJwt: 'internal-jwt', expiresAt: Date.now() + 3600000 };

    http.post('api-endpoint/account/authenticate', {}).subscribe();
    const req = httpMock.expectOne('api-endpoint/account/authenticate');
    expect(req.request.headers.has('Authorization')).toBe(false);
    req.flush({});
  });

  it('should attach internal JWT to business API requests', () => {
    storedSession = { internalJwt: 'internal-jwt', expiresAt: Date.now() + 3600000 };

    http.get('/api/users').subscribe();
    const req = httpMock.expectOne('/api/users');
    expect(req.request.headers.get('Authorization')).toBe('Bearer internal-jwt');
    req.flush([]);
  });

  it('should NOT attach any token when no session', () => {
    storedSession = null;

    http.get('/api/users').subscribe();
    const req = httpMock.expectOne('/api/users');
    expect(req.request.headers.has('Authorization')).toBe(false);
    req.flush([]);
  });
});
