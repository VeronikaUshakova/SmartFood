import { Component, OnInit } from '@angular/core';
import { Box } from 'src/app/shared/box.model';
import { BoxService } from 'src/app/shared/box.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-box-form',
  templateUrl: './box-form.component.html',
  styles: [
  ]
})
export class BoxFormComponent implements OnInit {

  constructor(public service: BoxService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id_box == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.postBox().subscribe(
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
    this.service.putBox().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info();
      },
      err => {    
        this.toastr.error(err.error.text);
      }
    );
  }


  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new Box();
  }
}

