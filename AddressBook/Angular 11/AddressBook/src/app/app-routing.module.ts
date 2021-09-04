import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// const routes: Routes = [
  
// ];

const routes: Routes = [
  {path: '', redirectTo: '/contacts', pathMatch: 'full'},
  {
    path: 'contacts',
    loadChildren: './contacts/contacts.module#ContactsModule'            
  },
  //{path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes,  {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
