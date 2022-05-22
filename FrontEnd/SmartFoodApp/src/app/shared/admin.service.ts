import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Shipper } from '../shared/shipper.model';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';
import { BackUp } from './back-up.model';

const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  constructor(private http: HttpClient) { }

  formApplyBackUp: BackUp = new BackUp();
  formCreateBackUp: BackUp = new BackUp();
  listShipper:Shipper[];

  public exportAsExcelFile(json: any[], excelFileName: string): void
  {
      const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);
      const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
      const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
      this.saveAsExcelFile(excelBuffer, excelFileName);
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {type: EXCEL_TYPE});
    FileSaver.saveAs(data, fileName + '_export_' + new  Date().getTime() + EXCEL_EXTENSION);
 }

  refreshCreateBackUp() {
    return this.http.get('https://localhost:44363/Admin/BackUp?str='+this.formCreateBackUp.file)
  }

  refreshApplyBackUp() {
    return this.http.get('https://localhost:44363/Admin/RestoreBackUp?str='+this.formApplyBackUp.file)
  }
  
  refreshListShipper(){
    this.http.get('https://localhost:44363/Admin/DownloadShipper')
      .toPromise()
      .then(res =>this.listShipper = res as Shipper[]);
  }

  exportShipper() {
    this.http.get('https://localhost:44363/Admin/DownloadShipper')
      .toPromise()
  }

  exportFoodEstablishment() {
    this.http.get('https://localhost:44363/Admin/DownloadFoodEstablishment')
      .toPromise()
  }

  exportProduct() {
    this.http.get('https://localhost:44363/Admin/DownloadProduct')
      .toPromise()
  }

  exportBox() {
    this.http.get('https://localhost:44363/Admin/DownloadBox')
      .toPromise()
  }

  exportHistoryBox() {
    this.http.get('https://localhost:44363/Admin/DownloadHistoryBox')
      .toPromise()
  }

  exportDelivery() {
    this.http.get('https://localhost:44363/Admin/DownloadDelivery')
      .toPromise()
  }
}