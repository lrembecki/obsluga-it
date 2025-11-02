import { HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { tap } from 'rxjs/operators';

export const imageCacheInterceptor: HttpInterceptorFn = (req, next) => {
  // Check if this is an image-related request
  const isImageRequest =
    req.url.includes('/images') ||
    req.url.includes('.blob.core.windows.net') ||
    req.headers.get('Accept')?.includes('image/');

  if (isImageRequest && req.method === 'GET') {
    // Add cache-friendly headers for image requests
    req = req.clone({
      setHeaders: {
        'Cache-Control': 'public, max-age=3600', // Cache for 1 hour
        'If-None-Match': '*', // Enable ETag support
      },
    });
  }

  return next(req).pipe(
    tap((event) => {
      if (event instanceof HttpResponse && isImageRequest) {
        // Log successful image cache responses
        if (event.status === 304) {
          console.log('Image served from cache:', req.url);
        }
      }
    }),
  );
};
