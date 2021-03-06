import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { ImagesComponent } from './components/images/images.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        ImagesComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'images', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'images', component: ImagesComponent },
            { path: '**', redirectTo: 'images' }
        ])
    ]
})
export class AppModuleShared {
}
