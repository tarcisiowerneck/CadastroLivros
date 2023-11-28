import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { BooksListComponent } from './core/components/books/books-list/books-list.component';
import { BooksAddComponent } from './core/components/books/books-add/books-add.component';
import { FormsModule } from '@angular/forms';
import { BooksEditComponent } from './core/components/books/books-edit/books-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    BooksListComponent,
    BooksAddComponent,
    BooksEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
