import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksListComponent } from './core/components/books/books-list/books-list.component';
import { BooksAddComponent } from './core/components/books/books-add/books-add.component';
import { BooksEditComponent } from './core/components/books/books-edit/books-edit.component';

const routes: Routes = [
  {
    path: 'books/list',
    component: BooksListComponent
  },
  {
    path: 'books/add',
    component: BooksAddComponent
  },
  {    
    path: 'books/edit/:cod',
    component: BooksEditComponent
  },
  {
    path: 'books/remove/:cod',
    component: BooksEditComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
