import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactListComponent} from './contact-list/contact-list.component';
import { ContactsComponent } from './contacts.component';

export const contactsRoutes: Routes = [{
    path: '',
    component: ContactsComponent,
    children: [
      {path: '', component: ContactListComponent},      
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(contactsRoutes)],
  exports: [RouterModule]
})
export class ContactsRoutingModule {    
 }
 
export const contactsRoutedComponents = [
    ContactListComponent,
  ];
