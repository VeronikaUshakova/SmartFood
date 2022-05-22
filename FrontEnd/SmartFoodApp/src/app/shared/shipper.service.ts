import { Injectable } from '@angular/core';
import { Shipper } from './shipper.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ShipperService {

  constructor(private http: HttpClient) { }

  formData: Shipper = new Shipper();
  list: Shipper[];

  postShipper() {
    return this.http.post('https://localhost:44363/Shipper/Create', this.formData);
  }

  putShipper() {
    return this.http.put(`https://localhost:44363/Shipper/Edit/${this.formData.id_shipper}`, this.formData);
  }

  deleteShipper(id: number) {
    return this.http.delete(`https://localhost:44363/Shipper/Delete/${id}`);
  }

  refreshList() {
    this.http.get('https://localhost:44363/Shipper/Index')
      .toPromise()
      .then(res =>this.list = res as Shipper[]);
  }

}