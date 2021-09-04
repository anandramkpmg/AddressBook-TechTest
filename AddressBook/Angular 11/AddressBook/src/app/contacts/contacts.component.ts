import {Component, OnInit} from '@angular/core';
//import {Store} from '@ngrx/store';
import {Router} from '@angular/router';
//import {AppState} from '../app.state';

@Component({
    selector: 'app-contacts',
    template: `
      <router-outlet></router-outlet>`,    
  })

  // @Component({
  //   selector: 'app-contacts',
  //   template: ``,    
  // })

  export class ContactsComponent implements OnInit {

    constructor(private router: Router) {
    }

    ngOnInit() {

    }
    actionSuccess(done: boolean, message: string) {
        if (done) {
          alert(message);
          this.router.navigate(['/contacts']);
        }
      }
  }  