import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Product } from './product.model';
import { Shipper } from './shipper.model'
import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';

@Injectable({
  providedIn: 'root'
})

export class ProductService {

  constructor(private http: HttpClient) { }

  formData: Product = new Product();
  list: Product[];
  listShipper: Shipper[];

  refreshListShipper() {
    this.http.get('https://localhost:44363/Shipper/Index')
      .toPromise()
      .then(res =>this.listShipper = res as Shipper[]);
  }

  postProduct() {
    this.formData.id_shipper = Number(this.formData.id_shipper);
    return this.http.post('https://localhost:44363/Product/Create', this.formData);
  }

  putProduct() {
    this.formData.id_shipper = Number(this.formData.id_shipper);
    return this.http.put(`https://localhost:44363/Product/Edit/${this.formData.id_product}`, this.formData);
  }

  deleteProduct(id: number) {
    return this.http.delete(`https://localhost:44363/Product/Delete/${id}`);
  }


  refreshList() {
    this.http.get('https://localhost:44363/Product/Index')
      .toPromise()
      .then(res =>this.list = res as Product[]);
  }
}