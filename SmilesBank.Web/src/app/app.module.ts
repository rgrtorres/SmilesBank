import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ExtractService } from 'src/services/Extract.services';
import { AppComponent } from './app.component';
import { ConfigService } from 'src/services/Config.Services';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

const appInitializerFn = (appConfig: ConfigService) => {
  return () => {
    return appConfig.loadAppConfig();
  };
};

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    FormsModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializerFn, multi: true, deps: [ConfigService] },
    ExtractService,
    ConfigService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
