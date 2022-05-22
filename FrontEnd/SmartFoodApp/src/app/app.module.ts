import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastNoAnimation, ToastrModule } from 'ngx-toastr';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { ShipperFormComponent } from './shipper/shipper-form/shipper-form.component';
import { HttpClientModule } from '@angular/common/http';
import { Browser } from 'selenium-webdriver';
import { FoodEstablishmentFormComponent } from './food-establishment/food-establishment-form/food-establishment-form.component';
import { ProductFormComponent } from './product/product-form/product-form.component';
import { BoxFormComponent } from './box/box-form/box-form.component';
import { HistoryBoxFormComponent } from './history-box/history-box-form/history-box-form.component';
import { DeliveryFormComponent } from './delivery/delivery-form/delivery-form.component';
import { BackupComponent } from './admin/backup/backup.component';
import { BusinessLogicShipperWeekComponent } from './business-logic/business-logic-shipper-week/business-logic-shipper-week.component';
import { BusinessLogicShipperMonthComponent } from './business-logic/business-logic-shipper-month/business-logic-shipper-month.component';
import { BusinessLogicCorrectStorageProductComponent } from './business-logic/business-logic-correct-storage-product/business-logic-correct-storage-product.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgCircleProgressModule } from 'ng-circle-progress';
import { AccumulationChartModule, PieSeriesService, AccumulationDataLabelService, AccumulationLegendService } from '@syncfusion/ej2-angular-charts';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    ShipperFormComponent,
    FoodEstablishmentFormComponent,
    ProductFormComponent,
    BoxFormComponent,
    HistoryBoxFormComponent,
    DeliveryFormComponent,
    BackupComponent,
    BusinessLogicShipperWeekComponent,
    BusinessLogicShipperMonthComponent,
    BusinessLogicCorrectStorageProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    BrowserModule,
    AccumulationChartModule,
    NgCircleProgressModule.forRoot({
      "radius": 60,
      "outerStrokeWidth": 10,
      "outerStrokeColor": "#00fdff",
      "innerStrokeColor": "#00fdff",
      "innerStrokeWidth": 5,
      "showBackground": false,
      "startFromZero": false
    }),
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    TranslateModule.forRoot(
      {
        loader:{
            provide: TranslateLoader,
            useFactory: HttpLoaderFactory,
            deps:[HttpClient]
        },
        defaultLanguage: 'en'
      }
    )
  ],
  providers: [PieSeriesService, AccumulationDataLabelService, AccumulationLegendService],
  bootstrap: [AppComponent]
})
export class AppModule { }
