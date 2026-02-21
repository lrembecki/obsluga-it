import { InjectionToken } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export const HTTP_CLIENT = new InjectionToken<HttpClient>('HTTP_CLIENT');
