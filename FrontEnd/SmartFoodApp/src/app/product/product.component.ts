import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../shared/product.model';
import { ProductService } from '../shared/product.service';
import * as XLSX from 'xlsx'; 

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styles: [
  ]
})
export class ProductComponent implements OnInit {

  constructor(public service:ProductService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
    this.service.refreshListShipper();
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

  populateForm(selectedRecord:Product){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Are you sure to delete this record?'))
    {
      this.service.deleteProduct(id)
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

