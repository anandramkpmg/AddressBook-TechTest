import { NgModule} from '@angular/core';
import { contactsRoutedComponents, ContactsRoutingModule } from './contacts-routing.module';
import { SharedModule } from '../shared/shared/shared.module';

@NgModule({  
  imports: [    
    SharedModule,
    ContactsRoutingModule,    
  ],
  declarations: [contactsRoutedComponents]  
})
export class ContactsModule { }
