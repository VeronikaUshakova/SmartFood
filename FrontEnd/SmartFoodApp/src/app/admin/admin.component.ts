import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from '../shared/admin.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styles: [
  ]
})
export class AdminComponent implements OnInit {

  constructor(public service:AdminService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshListShipper;
  }
}
