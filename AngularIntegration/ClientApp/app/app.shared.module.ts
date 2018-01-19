import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { TimeComponent } from './components/time/time.component'

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        TimeComponent
    ],
    imports: [
        CommonModule,
        HttpModule
    ]
})
export class AppModuleShared {
}
