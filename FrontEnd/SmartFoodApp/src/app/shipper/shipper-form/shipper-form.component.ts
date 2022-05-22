import { Component, OnInit } from '@angular/core';
import { ShipperService } from 'src/app/shared/shipper.service';
import { NgForm } from '@angular/forms';
import { Shipper } from 'src/app/shared/shipper.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-shipper-form',
  templateUrl: './shipper-form.component.html',
  styles: [
  ]
})
export class ShipperFormComponent implements OnInit {

  constructor(public service: ShipperService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id_shipper == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.postShipper().subscribe(
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
    this.service.putShipper().subscribe(
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
    this.service.formData = new Shipper();
  }

}
