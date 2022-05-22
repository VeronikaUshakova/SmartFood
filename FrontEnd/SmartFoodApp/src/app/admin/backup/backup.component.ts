import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/admin.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-backup',
  templateUrl: './backup.component.html',
  styles: [
  ]
})
export class BackupComponent implements OnInit {

  constructor(public service: AdminService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmitCreate(form: NgForm) {
      this.createBackUp();
  }

  onSubmitApply(form: NgForm) {
    this.applyBackUp();
}

  createBackUp() {
    this.service.refreshCreateBackUp().subscribe(
      res => {
        this.toastr.success()
      },
      err => { 
        this.toastr.error();
      }
    );
  }

  applyBackUp() {
    this.service.refreshApplyBackUp().subscribe(
      res => {
        this.toastr.success()
      },
      err => { 
        this.toastr.error();
      }
    );
  }

  onFileChange(event:any) {
  
    if (event.target.files.length > 0) {
      const file = event.target.files[0].name;
      this.service.formApplyBackUp.file = String(file);
    }
  }
}
