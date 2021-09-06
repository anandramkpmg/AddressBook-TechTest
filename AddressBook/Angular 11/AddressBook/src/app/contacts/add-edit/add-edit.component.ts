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
  //maxDate = new Date(2021, 8, 4);
  maxDate = new Date();

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
    console.log(this.form.value);
    this.contactService.create(this.form.value)
      .pipe(first())
      .subscribe(() => {
        this.alertService.success('User added', { keepAfterRouteChange: true });
        this.router.navigate(['../'], { relativeTo: this.route });
      })
      .add(() => this.loading = false);
  }

  private updateUser() {
    console.log(this.form.value);
    this.contactService.update(this.id, this.form.value)
      .pipe(first())
      .subscribe(() => {
        this.alertService.success('User updated', { keepAfterRouteChange: true });
        this.router.navigate(['../../'], { relativeTo: this.route });
      })
      .add(() => this.loading = false);
  }
}
