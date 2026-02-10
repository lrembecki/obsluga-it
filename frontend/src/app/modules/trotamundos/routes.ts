import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const facades = {
  advantages: await import('./advantages/advantage.facade').then(
    (e) => e.TrotamundosAdvantageFacade,
  ),
  files: await import('./files/file.facade').then(
    (e) => e.TrotamundosFileFacade,
  ),
  highlights: await import('./highlights/highlight.facade').then(
    (e) => e.TrotamundosHighlightFacade,
  ),
  loyalityProgram: await import(
    './loyality-program/loyality-program.facade'
  ).then((e) => e.TrotamundosLoyalityProgramFacade),
  trips: await import('./trips/trip.facade').then(
    (e) => e.TrotamundosTripFacade,
  ),
  aboutUs: await import('./about-us/about-us.facade').then(
    (e) => e.TrotamundosAboutUsFacade,
  ),
  howItWorks: await import('./how-it-works/how-it-works.facade').then(
    (e) => e.TrotamundosHowItWorksFacade,
  ),
  individualTrips: await import(
    './individual-trips/individual-trip.facade'
  ).then((e) => e.TrotamundosIndividualTripFacade),
};

export const routes: Routes = [
  featureRoute('advantages', 'Advantages', ['Trotamundos.Advantages'], () =>
    import('./advantages/routes').then((e) => e.routes),
  ),
  featureRoute('highlights', 'Highlights', ['Trotamundos.Highlights'], () =>
    import('./highlights/routes').then((e) => e.routes),
  ),
  featureRoute(
    'loyality-program',
    'Loyality Program',
    ['Trotamundos.Loyality-Program'],
    () => import('./loyality-program/routes').then((e) => e.routes),
  ),
  featureRoute('trips', 'Trips', ['Trotamundos.Trips'], () =>
    import('./trips/routes').then((e) => e.routes),
  ),
  featureRoute('files', 'Files', ['Trotamundos.Files'], () =>
    import('./files/routes').then((e) => e.routes),
  ),
  featureRoute('about-us', 'About Us', ['Trotamundos.Pages.AboutUs'], () =>
    import('./about-us/routes').then((e) => e.routes),
  ),
  featureRoute(
    'how-it-works',
    'How It Works',
    ['Trotamundos.Pages.HowItWorks'],
    () => import('./how-it-works/routes').then((e) => e.routes),
  ),
  featureRoute(
    'individual-trips',
    'Individual Trips',
    ['Trotamundos.IndividualTrips'],
    () => import('./individual-trips/routes').then((e) => e.routes),
  ),
];
