import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { ContactService } from '../../services/contact.service';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {
  form!: FormGroup;
  id!: string;
  isAddMode!: boolean;
  loading = false;
  submitted = false;
  dateTime = new Date();
  maxDate = new Date(this.dateTime.getUTCFullYear(), this.dateTime.getUTCMonth(), this.dateTime.getUTCDate() - 1);

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private contactService: ContactService,
    private alertService: AlertService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;

    const formOptions: AbstractControlOptions = {
    };
    this.form = this.formBuilder.group({
      firstName: ['', Validators.compose([Validators.required])],
      surName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      id: this.id == null ? 0 : this.id,
      dateOfBirth: ['', Validators.required],
    }, formOptions);

    if (!this.isAddMode) {
      this.contactService.getById(this.id)
        .pipe(first())
        .subscribe(x => this.form.patchValue(x));
    }
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    if (this.isAddMode) {
      this.createUser();
    } else {
      this.updateUser();
    }
  }

  private createUser() {
    this.form.value.dateOfBirth = this.getDate();    
    this.contactService.create(this.form.value)
      .pipe(first())
      .subscribe(() => {
        this.alertService.success('Contact added', { keepAfterRouteChange: true });
        this.router.navigate(['../'], { relativeTo: this.route });
      })
      .add(() => this.loading = false);
  }

  private updateUser() {    
    this.form.value.dateOfBirth = this.getDate();
    this.contactService.update(this.id, this.form.value)
      .pipe(first())
      .subscribe(() => {
        this.alertService.success('Contact updated', { keepAfterRouteChange: true });
        this.router.navigate(['../../'], { relativeTo: this.route });
      })
      .add(() => this.loading = false);
  }

  private getDate()
  {
    const selectedDate =  new Date(this.form.value.dateOfBirth);
    const today = new Date();
    const correctDate = new Date(selectedDate.getFullYear(), selectedDate.getMonth(), selectedDate.getDate(), today.getHours(), today.getMinutes(), today.getSeconds());
    console.log("Fixed Date", correctDate);
    return correctDate;
  }
}