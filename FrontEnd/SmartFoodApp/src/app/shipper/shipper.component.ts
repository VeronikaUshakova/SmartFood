import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Shipper } from '../shared/shipper.model';
import { ShipperService } from '../shared/shipper.service';
import * as XLSX from 'xlsx'; 

@Component({
  selector: 'app-shipper',
  templateUrl: './shipper.component.html',
  styles: [
  ]
})
export class ShipperComponent implements OnInit {

  constructor(public service:ShipperService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord:Shipper){
    this.service.formData = Object.assign({}, selectedRecord);
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

  onDelete(id:number){
    if(confirm('Are you sure to delete this record?'))
    {
      this.service.deleteShipper(id)
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
