import { Injectable } from '@angular/core';
import { Shipper } from './shipper.model';
import { FoodEstablishment } from './food-establishment.model';
import { HttpClient } from "@angular/common/http";
import { Product } from './product.model';

@Injectable({
  providedIn: 'root'
})
export class DiagramsService {

  constructor(private http: HttpClient) { }

  listShipper: Shipper[];
  listFoodEstablishment: FoodEstablishment[];
  listProduct: Product[];

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

  refreshListProduct() {
    this.http.get('https://localhost:44363/Product/Index')
      .toPromise()
      .then(res =>this.listProduct = res as Product[]);
  }
}
