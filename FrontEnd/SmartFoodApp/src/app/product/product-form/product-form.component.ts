import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/shared/product.service';
import { NgForm } from '@angular/forms';
import { Product } from 'src/app/shared/product.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styles: [
  ]
})
export class ProductFormComponent implements OnInit {

  constructor(public service: ProductService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id_product == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.postProduct().subscribe(
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
    this.service.putProduct().subscribe(
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
    this.service.formData = new Product();
  }
}
