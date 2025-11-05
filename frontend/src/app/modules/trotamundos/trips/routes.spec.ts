import { routes } from './routes';

describe('Trips Routes', () => {
  it('should define one list route', () => {
    expect(routes.length).toBe(1);
    const route = routes[0];
    // listRoute produces route with children for list/create/edit; just basic sanity checks
    expect(route).toBeTruthy();
    expect(typeof route.loadChildren).toBe('undefined');
    expect(route.children?.length).toBeGreaterThan(0);
  });
});
