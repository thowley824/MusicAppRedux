import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule, JsonpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavigationComponent } from './navigation/navigation.component';
import { HomeComponent } from './home/home.component';
import { AlbumTableComponent } from './albums/albumtable.component';
import { AlbumComponent } from './albums/albumdetail.component';

@NgModule({
    imports: [BrowserModule, HttpModule, JsonpModule, RouterModule.forRoot([
    { path: 'home', component: HomeComponent },
    { path: 'albums/:id', component: AlbumComponent },
    { path: 'albums', component: AlbumTableComponent }
    ])],

    declarations: [AppComponent, NavigationComponent, HomeComponent, AlbumTableComponent, AlbumComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
