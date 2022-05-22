import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShipperComponent } from './shipper/shipper.component';
import { FoodEstablishmentComponent } from './food-establishment/food-establishment.component';
import { ProductComponent } from './product/product.component';
import { BoxComponent } from './box/box.component';
import { DeliveryComponent } from './delivery/delivery.component';
import { HistoryBoxComponent } from './history-box/history-box.component';
import { AdminComponent } from './admin/admin.component';
import { BusinessLogicComponent } from './business-logic/business-logic.component';
import { DiagramsComponent } from './diagrams/diagrams.component';

const routes: Routes = [
  {path:'shippers', component: ShipperComponent },
  {path:'foodEstablishments', component: FoodEstablishmentComponent},
  {path:'products', component:ProductComponent},
  {path:'boxes', component:BoxComponent},
  {path:'deliveries', component:DeliveryComponent},
  {path:'historyBoxes', component:HistoryBoxComponent},
  {path:'backUp', component:AdminComponent},
  {path:'businessLogic', component: BusinessLogicComponent},
  {path:'diagrams', component: DiagramsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [ShipperComponent, FoodEstablishmentComponent, ProductComponent,
  BoxComponent, DeliveryComponent, HistoryBoxComponent, AdminComponent, BusinessLogicComponent,
  DiagramsComponent];
