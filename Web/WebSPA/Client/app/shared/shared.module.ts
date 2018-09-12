import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { JsonpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { EChartsDirective } from './directives/echarts.directive';
import { SlimScrollDirective } from './directives/slim-scroll.directive';

// Services
import { DataService } from './services/data.service';
import { SecurityService } from './services/security.service';
import { ConfigurationService } from './services/configuration.service';
import { StorageService } from './services/storage.service';
import { SignalrService } from './services/signalr.service';

// Pipes:
import { UppercasePipe } from './pipes/uppercase.pipe';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    JsonpModule
  ],
  declarations: [
    EChartsDirective,
    SlimScrollDirective,
    UppercasePipe
  ],
  exports: [
    EChartsDirective,
    SlimScrollDirective,
    //UppercasePipe,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
  ]
})

export class SharedModule {
  static forRoot(): ModuleWithProviders {
      return {
          ngModule: SharedModule,
          providers: [
              DataService,
              SecurityService,
              ConfigurationService,
              StorageService,
              SignalrService
          ]
      };
  }
}

