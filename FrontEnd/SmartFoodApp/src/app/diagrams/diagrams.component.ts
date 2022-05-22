import { Component, OnInit } from '@angular/core';
import { DiagramsService } from 'src/app/shared/diagrams.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-diagrams',
  templateUrl: './diagrams.component.html',
  styles: [
  ]
})

export class DiagramsComponent {
    public data:Object[];
    public chartLabel:Object;
    public legend:Object;

    public dataProduct:Object[];

    constructor(public service:DiagramsService,
        private toastr:ToastrService) { 
            this.service.refreshListShipper();
            this.service.refreshListFoodEstablishment();
            this.service.refreshListProduct();
            if(this.service.listShipper !== undefined && this.service.listFoodEstablishment !== undefined){
                let all = this.service.listFoodEstablishment.length + this.service.listShipper.length;
                    let shipperPercent = Math.round((this.service.listShipper.length*100)/all);
                    let foodEstablishmentPercent =   Math.round((this.service.listFoodEstablishment.length*100)/all);
                this.data = [
                    {name: 'Shipper', value: shipperPercent, text: shipperPercent.toString()+"%"},
                    {name: 'FoodEstablishment', value: foodEstablishmentPercent, text: foodEstablishmentPercent.toString()+"%"}
                ];
                this.chartLabel = {
                    visible: true,
                    position: 'Inside',
                    name: 'text'
                };
                this.legend = {
                    visible: true,
                    position: 'Bottom',
                    height: '10%',
                    width: '80%'
                }
            }

            if(this.service.listProduct !== undefined){
                let allProduct = 0;
                for(let i = 0;i< this.service.listProduct.length;i++){
                    allProduct += this.service.listProduct[i].weight_product;
                }
                this.dataProduct = [];
                for(let i = 0;i< this.service.listProduct.length;i++){
                    let percentProduct = Math.round((this.service.listProduct[i].weight_product * 100)/allProduct);
                    let obj = {name: this.service.listProduct[i].name_product, value: percentProduct, text: percentProduct.toString()+"%"}
                    this.dataProduct.push(obj);
                }
            }
    }
}

