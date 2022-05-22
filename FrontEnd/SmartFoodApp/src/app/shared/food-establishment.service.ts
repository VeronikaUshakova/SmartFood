import { Injectable } from '@angular/core';
import { FoodEstablishment } from './food-establishment.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class FoodEstablishmentService {

  constructor(private http: HttpClient) { }

  formData: FoodEstablishment = new FoodEstablishment();
  list: FoodEstablishment[];

  postFoodEstablishment() {
    return this.http.post('https://localhost:44363/FoodEstablishment/Create', this.formData);
  }

  putFoodEstablishment() {
    return this.http.put(`https://localhost:44363/FoodEstablishment/Edit/${this.formData.id_foodEstablishment}`, this.formData);
  }

  deleteFoodEstablishment(id: number) {
    return this.http.delete(`https://localhost:44363/FoodEstablishmentr/Delete/${id}`);
  }

  refreshList() {
    this.http.get('https://localhost:44363/FoodEstablishment/Index')
      .toPromise()
      .then(res =>this.list = res as FoodEstablishment[]);
  }

}