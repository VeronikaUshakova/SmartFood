import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Box } from '../shared/box.model';
import { BoxService } from '../shared/box.service';
import * as XLSX from 'xlsx'; 

@Component({
  selector: 'app-box',
  templateUrl: './box.component.html',
  styles: [
  ]
})
export class BoxComponent implements OnInit {

  constructor(public service:BoxService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
    this.service.refreshListProduct();
    this.service.refreshListDelivery();
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
  
  populateForm(selectedRecord:Box){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Are you sure to delete this record?'))
    {
      this.service.deleteBox(id)
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


