import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BusinessLogicService } from '../shared/business-logic.service';

@Component({
  selector: 'app-business-logic',
  templateUrl: './business-logic.component.html',
  styles: [
  ]
})
export class BusinessLogicComponent implements OnInit {

  constructor(public service:BusinessLogicService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshShipper();
    this.service.refreshFoodEstablishment();
    this.service.refreshProduct();
  }
  
}



