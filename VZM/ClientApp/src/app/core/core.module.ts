import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpService } from './http.service';
import { AppStorageService } from './app-storage.service';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [],
    providers: [
        HttpService,
        AppStorageService,
    ],
})
export class CoreModule { }
