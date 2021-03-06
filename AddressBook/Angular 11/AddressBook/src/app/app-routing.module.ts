import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

const contactsModule = () => import('./contacts/contacts.module').then(x => x.ContactsModule);

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'contacts', loadChildren: contactsModule },

    // otherwise redirect to home
    { path: '**', redirectTo: 'contacts' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
