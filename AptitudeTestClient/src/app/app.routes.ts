import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./home').then(c => c.Home)
  },
  {
    path: 'quotes',
    loadComponent: () => import('./quotes-list/quotes-list.component').then(c => c.QuotesListComponent)
  },
  {
    path: 'quotes/new',
    loadComponent: () => import('./create-quote/create-quote.component').then(c => c.CreateQuoteComponent)
  },
  {
    path: 'quotes/:id',
    loadComponent: () => import('./edit-quote/edit-quote.component').then(c => c.EditQuoteComponent)
  }
];
