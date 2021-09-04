import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../services/contact.service';

import { first } from 'rxjs/operators';
import { Contact } from 'src/app/models';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  contacts!: Contact[];
  constructor(private contactService: ContactService) { }

  ngOnInit() {
    this.contactService.getAll()
      .pipe(first())
      .subscribe((contacts:any) => this.contacts = contacts);
  }
}
