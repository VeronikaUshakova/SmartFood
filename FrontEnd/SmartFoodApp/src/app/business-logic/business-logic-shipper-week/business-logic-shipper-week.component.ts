import { Component, OnInit } from '@angular/core';
import { BusinessLogicService } from 'src/app/shared/business-logic.service';
import { NgForm } from '@angular/forms';
import { PercentWeekMonth } from 'src/app/shared/percent-week-month.model';
import { ToastrService } from 'ngx-toastr';
import { NgCircleProgressModule } from 'ng-circle-progress';

@Component({
  selector: 'app-business-logic-shipper-week',
  templateUrl: './business-logic-shipper-week.component.html',
  styles: [
  ]
})

export class BusinessLogicShipperWeekComponent implements OnInit {
  percentValue: number;
  constructor(public service: BusinessLogicService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.percentValue=0;
  }

  onSubmit(form: NgForm) {
    this.percentWeek();
  }

  percentWeek() {
    this.service.refreshPercentWeek().subscribe(
      res => {
        this.percentValue = Number(res.toString());
      },
      err => { 
        this.percentValue = 0;
      }
    );
  }
}
