import { Component, OnInit } from '@angular/core';
import { DeliveryService } from 'src/app/shared/delivery.service';
import { NgForm } from '@angular/forms';
import { Delivery } from 'src/app/shared/delivery.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delivery-form',
  templateUrl: './delivery-form.component.html',
  styles: [
  ]
})
export class DeliveryFormComponent implements OnInit {

  constructor(public service: DeliveryService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id_delivery == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.postDelivery().subscribe(
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
    this.service.putDelivery().subscribe(
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
    this.service.formData = new Delivery();
  }
}
