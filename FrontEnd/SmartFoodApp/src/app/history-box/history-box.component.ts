import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HistoryBox } from '../shared/history-box.model';
import { HistoryBoxService } from '../shared/history-box.service';
import * as XLSX from 'xlsx'; 

@Component({
  selector: 'app-history-box',
  templateUrl: './history-box.component.html',
  styles: [
  ]
})
export class HistoryBoxComponent implements OnInit {

  constructor(public service:HistoryBoxService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
    this.service.refreshListBox();
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
  populateForm(selectedRecord:HistoryBox){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Are you sure to delete this record?'))
    {
      this.service.deleteHistoryBox(id)
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


