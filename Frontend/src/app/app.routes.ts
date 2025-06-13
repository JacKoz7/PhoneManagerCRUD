import { Routes } from '@angular/router';
import { PhoneListComponent } from './components/phone-list/phone-list';
import { PhoneFormComponent } from './components/phone-form/phone-form';
import { PhoneDetailComponent } from './components/phone-detail/phone-detail';

export const routes: Routes = [
  { path: '', redirectTo: 'phones', pathMatch: 'full' },
  { path: 'phones', component: PhoneListComponent },
  { path: 'phones/new', component: PhoneFormComponent },
  { path: 'phones/:id', component: PhoneDetailComponent },
  { path: 'phones/:id/edit', component: PhoneFormComponent },
  { path: '**', redirectTo: 'phones' }
];