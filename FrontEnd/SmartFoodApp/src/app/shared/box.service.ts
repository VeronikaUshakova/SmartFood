import { Injectable } from '@angular/core';
import { Box } from './box.model';
import { HttpClient } from "@angular/common/http";
import { Product } from './product.model';
import { Delivery } from './delivery.model';

@Injectable({
  providedIn: 'root'
})
export class BoxService {
  constructor(private http: HttpClient) { }

  formData: Box = new Box();
  list: Box[];
  listProduct:Product[];
  listDelivery:Delivery[];

  refreshListProduct() {
    this.http.get('https://localhost:44363/Product/Index')
      .toPromise()
      .then(res =>this.listProduct = res as Product[]);
  }

  refreshListDelivery() {
    this.http.get('https://localhost:44363/Delivery/Index')
      .toPromise()
      .then(res =>this.listDelivery = res as Delivery[]);
  }

  postBox() {
    this.formData.id_delivery = Number(this.formData.id_delivery);
    this.formData.id_product = Number(this.formData.id_product);
    return this.http.post('https://localhost:44363/Box/Create', this.formData);
  }

  putBox() {
    this.formData.id_delivery = Number(this.formData.id_delivery);
    this.formData.id_product = Number(this.formData.id_product);
    return this.http.put(`https://localhost:44363/Box/Edit/${this.formData.id_box}`, this.formData);
  }

  deleteBox(id: number) {
    return this.http.delete(`https://localhost:44363/Box/Delete/${id}`);
  }

  refreshList() {
    this.http.get('https://localhost:44363/Box/Index')
      .toPromise()
      .then(res =>this.list = res as Box[]);
  }

}