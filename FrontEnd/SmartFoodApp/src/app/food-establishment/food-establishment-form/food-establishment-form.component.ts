import { Component, OnInit } from '@angular/core';
import { FoodEstablishmentService } from 'src/app/shared/food-establishment.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { FoodEstablishment } from 'src/app/shared/food-establishment.model';

@Component({
  selector: 'app-food-establishment-form',
  templateUrl: './food-establishment-form.component.html',
  styles: [
  ]
})
export class FoodEstablishmentFormComponent implements OnInit {

  constructor(public service: FoodEstablishmentService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id_foodEstablishment == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.postFoodEstablishment().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success()
      },
      err => {
        this.toastr.error(err.error.text);
      }
    );
  }

  updateRecord(form: NgForm) {
    this.service.putFoodEstablishment().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info()
      },
      err => {
        this.toastr.error(err.error.text);
      }
    );
  }


  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new FoodEstablishment();
  }

}
