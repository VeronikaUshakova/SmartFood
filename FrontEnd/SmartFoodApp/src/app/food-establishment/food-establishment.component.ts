import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FoodEstablishment } from '../shared/food-establishment.model';
import { FoodEstablishmentService } from '../shared/food-establishment.service';
import * as XLSX from 'xlsx'; 

@Component({
  selector: 'app-food-establishment',
  templateUrl: './food-establishment.component.html',
  styles: [
  ]
})
export class FoodEstablishmentComponent implements OnInit {

  constructor(public service:FoodEstablishmentService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
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
  
  populateForm(selectedRecord:FoodEstablishment){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Are you sure to delete this record?'))
    {
      this.service.deleteFoodEstablishment(id)
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
