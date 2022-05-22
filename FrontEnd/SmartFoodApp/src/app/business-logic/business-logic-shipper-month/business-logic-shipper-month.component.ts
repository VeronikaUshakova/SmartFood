import { Component, OnInit } from '@angular/core';
import { BusinessLogicService } from 'src/app/shared/business-logic.service';
import { NgForm } from '@angular/forms';
import { PercentWeekMonth } from 'src/app/shared/percent-week-month.model';
import { ToastrService } from 'ngx-toastr';
import { NgCircleProgressModule } from 'ng-circle-progress';


@Component({
  selector: 'app-business-logic-shipper-month',
  templateUrl: './business-logic-shipper-month.component.html',
  styles: [
  ]
})
export class BusinessLogicShipperMonthComponent implements OnInit {
  percentValue: number;

  constructor(public service: BusinessLogicService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.percentValue = 0;
  }

  onSubmit(form: NgForm) {
    this.percentMonth();
  }

  percentMonth() {
    this.service.refreshPercentMonth().subscribe(
      res => {
        this.percentValue = Number(res.toString());
      },
      err => { 
        this.percentValue = 0;
      }
    );
  }
}
