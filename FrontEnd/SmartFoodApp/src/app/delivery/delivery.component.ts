import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Delivery } from '../shared/delivery.model';
import { DeliveryService } from '../shared/delivery.service';
import * as XLSX from 'xlsx'; 

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styles: [
  ]
})
export class DeliveryComponent implements OnInit {

  constructor(public service:DeliveryService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
    this.service.refreshListShipper();
    this.service.refreshListFoodEstablishment();
  }
  fileName= 'ExcelSheet.xlsx';  

  exportexcel(): void 
  {
      let element = document.getElementById('table'); 
      const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
      XLSX.writeFile(wb, this.fileName);
  }
  
  populateForm(selectedRecord:Delivery){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Are you sure to delete this record?'))
    {
      this.service.deleteDelivery(id)
      .subscribe(
        res=>{
          this.service.refreshList();
          this.toastr.error()
        },
        err=>{
          this.toastr.error(err.error.text);
        }
      );
   }
  }
}



