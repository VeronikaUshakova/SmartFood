import { Injectable } from '@angular/core';
import { Delivery } from './delivery.model';
import { Shipper } from './shipper.model';
import { FoodEstablishment } from './food-establishment.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  constructor(private http: HttpClient) { }

  formData: Delivery = new Delivery();
  list: Delivery[];
  listShipper: Shipper[];
  listFoodEstablishment: FoodEstablishment[];

  refreshListShipper() {
    this.http.get('https://localhost:44363/Shipper/Index')
      .toPromise()
      .then(res =>this.listShipper = res as Shipper[]);
  }

  refreshListFoodEstablishment() {
    this.http.get('https://localhost:44363/FoodEstablishment/Index')
      .toPromise()
      .then(res =>this.listFoodEstablishment = res as FoodEstablishment[]);
  }

  postDelivery() {
    this.formData.id_shipper = Number(this.formData.id_shipper);
    this.formData.id_foodEstablishment = Number(this.formData.id_foodEstablishment);
    return this.http.post('https://localhost:44363/Delivery/Create', this.formData);
  }

  putDelivery() {
    this.formData.id_shipper = Number(this.formData.id_shipper);
    this.formData.id_foodEstablishment = Number(this.formData.id_foodEstablishment);
    return this.http.put(`https://localhost:44363/Delivery/Edit/${this.formData.id_delivery}`, this.formData);
  }

  deleteDelivery(id: number) {
    return this.http.delete(`https://localhost:44363/Delivery/Delete/${id}`);
  }

  refreshList() {
    this.http.get('https://localhost:44363/Delivery/Index')
      .toPromise()
      .then(res =>this.list = res as Delivery[]);
  }

}
