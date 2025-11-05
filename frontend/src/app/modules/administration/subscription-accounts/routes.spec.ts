import { routes } from './routes';

describe('Security Subscription Accounts Routes', () => {
  it('should export single list route configuration', () => {
    expect(routes.length).toBe(1);
  });
});
