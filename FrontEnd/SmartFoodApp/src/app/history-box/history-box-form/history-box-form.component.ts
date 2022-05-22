import { Component, OnInit } from '@angular/core';
import { HistoryBox } from 'src/app/shared/history-box.model';
import { HistoryBoxService } from 'src/app/shared/history-box.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-history-box-form',
  templateUrl: './history-box-form.component.html',
  styles: [
  ]
})
export class HistoryBoxFormComponent implements OnInit {

  constructor(public service: HistoryBoxService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id_history == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.postHistoryBox().subscribe(
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
    this.service.putHistoryBox().subscribe(
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
    this.service.formData = new HistoryBox();
  }
}

