import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Shipper } from "../shared/shipper.model";
import { Product } from "../shared/product.model";
import { PercentWeekMonth } from './percent-week-month.model';
import { FoodEstablishment } from './food-establishment.model';
import { PercentUseProduct } from './percent-use-product.model';

@Injectable({
  providedIn: 'root'
})
export class BusinessLogicService {

  constructor(private http: HttpClient) { }

  formDataWeek: PercentWeekMonth = new PercentWeekMonth();
  formDataMonth: PercentWeekMonth = new PercentWeekMonth();
  formUseProduct: PercentUseProduct = new PercentUseProduct();
  listShipper:Shipper[];
  listFoodEstablishment: FoodEstablishment[];
  listProduct: Product[];

  refreshPercentWeek() {
    return this.http.get('https://localhost:44363/Shipper/PercentWeek/'+this.formDataWeek.id_shipper);
  }

  refreshPercentMonth() {
    return this.http.get('https://localhost:44363/Shipper/PercentMonth/'+this.formDataMonth.id_shipper)
  }

  refreshUseProduct() {
    return this.http.get('https://localhost:44363/FoodEstablishment/PercentCorrectStorageProduct?product_id='+this.formUseProduct.id_product+'&foodEstablishment_id='+this.formUseProduct.id_foodEstablishment)
  }

  refreshFoodEstablishment() {
    this.http.get('https://localhost:44363/FoodEstablishment/Index')
    .toPromise()
    .then(res =>this.listFoodEstablishment = res as FoodEstablishment[]);
  }

  refreshProduct() {
    this.http.get('https://localhost:44363/Product/Index')
    .toPromise()
    .then(res =>this.listProduct = res as Product[]);
  }

  refreshShipper() {
    this.http.get('https://localhost:44363/Shipper/Index')
      .toPromise()
      .then(res =>this.listShipper = res as Shipper[]);
  }
}
