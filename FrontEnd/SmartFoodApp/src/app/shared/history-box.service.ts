import { Injectable } from '@angular/core';
import { HistoryBox } from './history-box.model';
import { Box } from './box.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class HistoryBoxService {

  constructor(private http: HttpClient) { }

  formData: HistoryBox = new HistoryBox();
  list: HistoryBox[];
  listBox: Box[];

  refreshListBox() {
    this.http.get('https://localhost:44363/Box/Index')
      .toPromise()
      .then(res =>this.listBox = res as Box[]);
  }

  postHistoryBox() {
    this.formData.id_box = Number(this.formData.id_box);
    return this.http.post('https://localhost:44363/HistoryBox/Create', this.formData);
  }

  putHistoryBox() {
    this.formData.id_box = Number(this.formData.id_box);
    return this.http.put(`https://localhost:44363/HistoryBox/Edit/${this.formData.id_history}`, this.formData);
  }

  deleteHistoryBox(id: number) {
    return this.http.delete(`https://localhost:44363/HistoryBox/Delete/${id}`);
  }

  refreshList() {
    this.http.get('https://localhost:44363/HistoryBox/Index')
      .toPromise()
      .then(res =>this.list = res as HistoryBox[]);
  }
}
